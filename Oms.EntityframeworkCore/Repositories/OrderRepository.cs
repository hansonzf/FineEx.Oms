using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Oms.EntityframeworkCore.Repositories
{
    public class OrderRepository : EfCoreRepository<OmsDbContext, BusinessOrder, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public async Task<IEnumerable<BusinessOrder>> GetOrdersByBusinessType(IEnumerable<long> ids, BusinessTypes businessType)
        {
            var context = await GetDbContextAsync();
            return businessType switch
            {
                BusinessTypes.OutboundWithTransport => await context.Set<OutboundOrder>().Where(o => ids.Contains(o.OutboundId)).ToListAsync(),
                BusinessTypes.InboundWithTransport => await context.Set<InboundOrder>().Where(o => ids.Contains(o.InboundId)).ToListAsync(),
                BusinessTypes.Transport => await context.Set<TransportOrder>().Where(o => ids.Contains(o.TransportId)).ToListAsync(),
                _ => new List<BusinessOrder>(),
            };
        }

        public async Task<IEnumerable<BusinessOrder>> GetWithCombinedOrdersAsync<TOrder>(Guid orderId)
            where TOrder : BusinessOrder
        {
            var context = await GetDbContextAsync();
            switch (typeof(TOrder).Name)
            {
                case "OutboundOrder":
                    var masterOrder = await context.Set<OutboundOrder>().SingleOrDefaultAsync(o => o.Id == orderId);
                    if (masterOrder is null)
                        return Array.Empty<OutboundOrder>();

                    if (masterOrder.RelationType == RelationTypes.CombinedMaster)
                    {
                        string idString = orderId.ToString().ToLower();
                        var orders = await context.Set<OutboundOrder>().Include(o => o.Details).Where(
                            o => EF.Property<string>(o, "_relatedOrderIds") == idString &&
                            o.RelationType == RelationTypes.CombinedSlave).ToListAsync() ?? new List<OutboundOrder>();
                        orders.Insert(0, masterOrder);

                        return orders;
                    }

                    return new OutboundOrder[1] { masterOrder };
                case "InboundOrder":
                case "TransportOrder":
                default:
                    return Array.Empty<OutboundOrder>();
            }
        }

        public async Task<BusinessOrder?> GetOrderByIdAsync(long id, BusinessTypes businessType)
        {
            var context = await GetDbContextAsync();
            return businessType switch
            {
                BusinessTypes.InboundWithTransport => await context.Set<InboundOrder>().Where(o => o.InboundId == id).FirstOrDefaultAsync(),
                BusinessTypes.OutboundWithTransport => await context.Set<OutboundOrder>().Where(o => o.OutboundId == id).FirstOrDefaultAsync(),
                BusinessTypes.Transport => await context.Set<TransportOrder>().Where(o => o.TransportId == id).FirstOrDefaultAsync(),
                _ => throw new ArgumentException($"The {Enum.GetName(businessType)} is not supported"),
            };
        }

        public async Task<TOrder?> GetAsync<TOrder>(Guid orderId)
            where TOrder : BusinessOrder
        {
            var context = await GetDbContextAsync();
            return await context.Set<TOrder>().SingleOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Guid?> GetOrderUuidByOrderIdAsync(long orderId, BusinessTypes businessType)
        {
            var context = await GetDbContextAsync();
            return businessType switch
            {
                BusinessTypes.InboundWithTransport => await context.Set<InboundOrder>().Where(o => o.InboundId == orderId).Select(o => o.Id).FirstOrDefaultAsync(),
                BusinessTypes.OutboundWithTransport => await context.Set<OutboundOrder>().Where(o => o.OutboundId == orderId).Select(o => o.Id).FirstOrDefaultAsync(),
                BusinessTypes.Transport => await context.Set<TransportOrder>().Where(o => o.TransportId == orderId).Select(o => o.Id).FirstOrDefaultAsync(),
                _ => throw new ArgumentException($"The {Enum.GetName(businessType)} is not supported"),
            };
        }

        public async Task<long?> GetOrderIdByOrderUuidAsync(Guid orderUuid, BusinessTypes businessType)
        {
            var context = await GetDbContextAsync();
            return businessType switch
            {
                BusinessTypes.InboundWithTransport => await context.Set<InboundOrder>().Where(o => o.Id == orderUuid).Select(o => o.InboundId).FirstOrDefaultAsync(),
                BusinessTypes.OutboundWithTransport => await context.Set<OutboundOrder>().Where(o => o.Id == orderUuid).Select(o => o.OutboundId).FirstOrDefaultAsync(),
                BusinessTypes.Transport => await context.Set<TransportOrder>().Where(o => o.Id == orderUuid).Select(o => o.TransportId).FirstOrDefaultAsync(),
                _ => throw new ArgumentException($"The {Enum.GetName(businessType)} is not supported"),
            };
        }

        private async Task<Guid?> GetOrderUuidByOrderNumberAsync<TOrder>(string orderNumber)
            where TOrder : BusinessOrder
        {
            var context = await GetDbContextAsync();

            return await context.Set<TOrder>().Where(o => o.OrderNumber == orderNumber).Select(o => o.Id).FirstOrDefaultAsync();
        }

        public async Task<TOrder?> GetAsync<TOrder>(string orderNumber)
            where TOrder : BusinessOrder
        {
            if (string.IsNullOrEmpty(orderNumber))
                return null;

            Guid? uuid = await GetOrderUuidByOrderNumberAsync<TOrder>(orderNumber);
            if (!uuid.HasValue)
                return null;

            return await GetAsync<TOrder>(uuid.Value);
        }

        public async Task<TOrder?> GetAsync<TOrder>(long orderId)
            where TOrder : BusinessOrder
        {
            if (orderId <= 0)
                return null;

            var context = await GetDbContextAsync();
            BusinessTypes bizType = (typeof(TOrder).Name) switch
            {
                "InboundOrder" => BusinessTypes.InboundWithTransport,
                "OutboundOrder" => BusinessTypes.OutboundWithTransport,
                "TransportOrder" => BusinessTypes.Transport,
                _ => throw new ArgumentException($"The {typeof(TOrder).Name} is not supported"),
            };
            Guid? uuid = await GetOrderUuidByOrderIdAsync(orderId, bizType);
            if (!uuid.HasValue)
                return null;

            return await GetAsync<TOrder>(uuid.Value);
        }

        public async Task<TOrder> InsertOrderAsync<TOrder>(TOrder order)
            where TOrder : BusinessOrder
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var context = await GetDbContextAsync();
            await context.Set<TOrder>().AddAsync(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> UpdateOrderAsync<TOrder>(TOrder order)
            where TOrder : BusinessOrder
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var context = await GetDbContextAsync();
            var entry = context.Set<TOrder>().Update(order);
            bool success = entry.State == EntityState.Modified;
            await context.SaveChangesAsync();

            return success;
        }

        public async Task<string> GetRelatedOrderIds(Guid masterOrderId, BusinessTypes businessType)
        { 
            if (businessType != BusinessTypes.OutboundWithTransport)
                return string.Empty;

            var context = await GetDbContextAsync();
            var relatedIds = await context.Set<OutboundOrder>().Where(o => o.Id == masterOrderId).Select(o => EF.Property<string>(o, "_relatedOrderIds")).ToListAsync();
            if (relatedIds.Any())
                return relatedIds.First();
            else
                return string.Empty;
        }

        public async Task<string> GetOrderTenantIdAsync(Guid orderId, BusinessTypes businessType)
        {
            throw new NotImplementedException();
        }

        //public async Task ReceivedTmsAcknowledgement(Guid orderId, BusinessTypes businessType, string transOrderNumber)
        //{
        //    string tableName = businessType switch
        //    {
        //        BusinessTypes.Transport => "TransportOrders",
        //        BusinessTypes.InboundWithTransport => "InboundOrders",
        //        BusinessTypes.OutboundWithTransport => "OutboundOrders",
        //        _ => throw new ArgumentException($"Unsupported business type {Enum.GetName(businessType)}")
        //    };
        //    string cmd = $"UPDATE {tableName} SET TmsOrderNumber = {transOrderNumber} WHERE Id = '{orderId}'";

        //    var context = await GetDbContextAsync();
        //    await context.Database.ExecuteSqlRawAsync(cmd);
        //}

        //public async Task ReceivedWmsAcknowledgement(Guid orderId, BusinessTypes businessType, string wmsOrderNumber)
        //{
        //    string tableName = businessType switch
        //    {
        //        BusinessTypes.Transport => "TransportOrders",
        //        BusinessTypes.InboundWithTransport => "InboundOrders",
        //        BusinessTypes.OutboundWithTransport => "OutboundOrders",
        //        _ => throw new ArgumentException($"Unsupported business type {Enum.GetName(businessType)}")
        //    };
        //    string cmd = $"UPDATE {tableName} SET WmsOrderNumber = {wmsOrderNumber} WHERE Id = '{orderId}'";

        //    var context = await GetDbContextAsync();
        //    await context.Database.ExecuteSqlRawAsync(cmd);
        //}

        public async Task<bool> ScheduledJobByOrder<TOrder>(Guid orderId, ProcessingSteps step)
                where TOrder : BusinessOrder
            {
                var context = await GetDbContextAsync();
                OrderStatus newState = step switch 
                { 
                    ProcessingSteps.B2bCheckoutInventory => OrderStatus.CheckingStock,
                    ProcessingSteps.MatchTransport => OrderStatus.MatchingTransportLine,
                    ProcessingSteps.Dispatching => OrderStatus.WaitforDispatch,
                    _ => throw new ArgumentException("Argumnet 'step' has wrong value")
                };
                string idString = orderId.ToString().ToLower();

                int affectRows = await context.Set<TOrder>()
                    .Where(o => o.Id == orderId || EF.Property<string>(o, "_relatedOrderIds") == idString)
                    .ExecuteUpdateAsync(p => p.SetProperty(o => o.OrderState, newState));

                return affectRows > 0;
            }

            //public async Task<bool> ScheduledJobByOrder(Guid orderId, BusinessTypes businessType, ProcessingSteps step)
            //{
            //    var context = await GetDbContextAsync();
            //    string tblName = businessType switch
            //    {
            //        BusinessTypes.OutboundWithTransport => "OutboundOrders",
            //        BusinessTypes.InboundWithTransport => "InboundOrders",
            //        BusinessTypes.Transport => "TransportOrders",
            //        _ => throw new ArgumentException("Argumnet 'businessType' has wrong value")
            //    };
            //    OrderStatus newState = step switch
            //    {
            //        ProcessingSteps.B2bCheckoutInventory => OrderStatus.CheckingStock,
            //        ProcessingSteps.MatchTransport => OrderStatus.MatchingTransportLine,
            //        ProcessingSteps.Dispatching => OrderStatus.WaitforDispatch,
            //        _ => throw new ArgumentException("Argumnet 'step' has wrong value")
            //    };
            //    string idString = orderId.ToString().ToLower();
            //    string cmd = $"UPDATE {tblName} SET OrderState={(int)newState} WHERE Id='{orderId}' OR RelateOrderIds='{idString}'";
            //    int affectRows = await context.Database.ExecuteSqlRawAsync(cmd);

            //    return affectRows > 0;
            //}

    }
}
