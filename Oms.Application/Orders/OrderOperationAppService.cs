using Oms.Application.Contracts;
using Oms.Domain.Orders;
using Oms.Domain.Shared.Orders.Events;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;

namespace Oms.Application.Orders
{
    public class OrderOperationAppService : ApplicationService, IOrderOperationAppService
    {
        readonly IOrderRepository repository;
        readonly ILocalEventBus localEventBus;

        public OrderOperationAppService(IOrderRepository repository, ILocalEventBus localEventBus)
        {
            this.repository = repository;
            this.localEventBus = localEventBus;
        }
        public async Task<BusinessOrderDto?> GetOrderByIdAsync(long orderId, BusinessTypes businessType)
        {
            var order = await GetOrderAsync(orderId, businessType);
            if (order is not null)
            {
                switch (businessType)
                {

                    case BusinessTypes.OutboundWithTransport:
                        var outboundOrder = order as OutboundOrder;
                        return ObjectMapper.Map<OutboundOrder, OutboundOrderDto>(outboundOrder);
                    case BusinessTypes.InboundWithTransport:
                        var inboundOrder = order as InboundOrder;
                        return ObjectMapper.Map<InboundOrder, InboundOrderDto>(inboundOrder);
                    case BusinessTypes.Transport:
                        var transportOrder = order as TransportOrder;
                        return ObjectMapper.Map<TransportOrder, TransportOrderDto>(transportOrder);
                    case BusinessTypes.None:
                    default:
                        return null;
                }
            }

            return null;
        }

        public async Task<ServiceResult> CheckStockAsync(IEnumerable<long> orderIds, BusinessTypes businessType)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'orderIds' does not contains any elements" };

            var orders = await repository.GetOrdersByBusinessType(orderIds, businessType);
            var outboundOrders = orders.OfType<OutboundOrder>();
            WarehouseDescription warehouse = outboundOrders.FirstOrDefault()?.Warehouse;
            if (warehouse is null)
                return new ServiceResult { Success = false, Message = "Can not found the warehouse of outbound orders" };

            foreach (var order in orders)
            {
                order.CheckInventory();
            }

            await localEventBus.PublishAsync(new CheckInventoryforOrderEvent { 
                OrderIds = orderIds,
                BusinessType = businessType
            });
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> MatchingTransportStrategyAsync(IEnumerable<long> orderIds, BusinessTypes businessType)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'orderIds' does not contains any elements" };

            var orders = await repository.GetOrdersByBusinessType(orderIds, businessType);
            foreach (var order in orders)
            {
                order.MatchTransportStrategy();
            }

            await localEventBus.PublishAsync(new MatchTransportLineForOrdersEvent
            {
                OrderIds = orderIds,
                BusinessType = businessType
            });

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> DispatchOrdersAsync(string tenantId, IEnumerable<long> orderIds, BusinessTypes businessType)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'orderIds' does not contains any elements" };

            var orders = await repository.GetOrdersByBusinessType(orderIds, businessType);
            foreach (var order in orders)
            {
                order.Dispatching();
            }

            await localEventBus.PublishAsync(
                new DispatchOrdersEvent
                {
                    OrderIds = orderIds.ToArray(),
                    TenantId = tenantId,
                    BusinessType = businessType
                });

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> UndoDispatchOrdersAsync(string tenantId, IEnumerable<long> orderIds, BusinessTypes businessType)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'orderIds' does not contains any elements" };

            var orders = await repository.GetOrdersByBusinessType(orderIds, businessType);
            foreach (var order in orders)
            {
                order.WithdrawDispatch();
            }

            await localEventBus.PublishAsync(
                new UndoDispatchOrdersEvent
                {
                    OrderIds = orderIds.ToArray(),
                    TenantId = tenantId,
                    BusinessType = BusinessTypes.Transport
                });

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> CancelOrderAsync(IEnumerable<long> orderIds, BusinessTypes businessType)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'orderIds' does not contains any elements" };

            var orders = await repository.GetOrdersByBusinessType(orderIds, businessType);
            foreach (var order in orders)
            {
                order.Cancel();
            }

            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new ServiceResult { Success = true };
        }

        private async Task<BusinessOrder?> GetOrderAsync(long orderId, BusinessTypes businessType)
        {
            return businessType switch
            {
                BusinessTypes.InboundWithTransport => await repository.GetAsync<InboundOrder>(orderId),
                BusinessTypes.OutboundWithTransport => await repository.GetAsync<OutboundOrder>(orderId),
                BusinessTypes.Transport => await repository.GetAsync<TransportOrder>(orderId),
                _ => null
            };
        }

        public async Task<ServiceResult> SetMatchedTransportStrategyAsync(long orderId, int matchType, BusinessTypes businessType, string strategyName, string strategyMemo, IEnumerable<TransportResource> resources)
        {
            var order = await GetOrderAsync(orderId, businessType);
            if (order is null)
                return new ServiceResult { Success = false, Message = "Order not be found" };

            var strategy = new TransportStrategy(
                strategyName,
                strategyMemo,
                matchType,
                resources.Select(
                    r => new TransportResource
                    {
                        ResourceId = r.ResourceId,
                        Name = r.Name,
                        Type = r.Type,
                        Address = r.Address,
                        City = r.City,
                        Contact = r.Contact,
                        District = r.District,
                        Phone = r.Phone,
                        Province = r.Province 
                    }).ToList());
            try
            {
                order.SetMatchedResult(strategy);
                await UnitOfWorkManager.Current.SaveChangesAsync();
                
                return new ServiceResult { Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, Error = ex };
            }
        }
    }
}
