using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Orders.MatchTransportLineForOrdersHandlers
{
    internal class AutomaticMatchTransportHandler
        : ILocalEventHandler<MatchTransportLineForOrdersEvent>, ITransientDependency
    {
        readonly IThreePLService dataService;
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uom;

        public AutomaticMatchTransportHandler(IThreePLService dataService, IOrderRepository repository, IUnitOfWorkManager uom)
        {
            this.dataService = dataService;
            this.repository = repository;
            this.uom = uom;
        }

        public async Task HandleEventAsync(MatchTransportLineForOrdersEvent eventData)
        {
            if (eventData.OrderId.HasValue)
            {
                await AutomaticMatchTransportPlan(eventData.TenantId, eventData.OrderId.Value, eventData.BusinessType);
            }
            else
            {
                // 手动执行，可批量执行，通过数字id查找
                foreach (var id in eventData.OrderIds)
                {
                    Guid? orderid = await repository.GetOrderUuidByOrderIdAsync(id, eventData.BusinessType);
                    if (orderid == null)
                        continue;
                    await AutomaticMatchTransportPlan(eventData.TenantId, orderid.Value, eventData.BusinessType);
                }
            }
            throw new NotImplementedException();
        }

        async Task AutomaticMatchTransportPlan(string tenantId, Guid orderId, BusinessTypes businessType)
        {
            bool result = false;
            switch (businessType)
            {

                case BusinessTypes.OutboundWithTransport:
                    result = await HandleOutboundOrder(orderId, tenantId);
                    break;
                case BusinessTypes.InboundWithTransport:
                    result = await HandleInboundOrder(orderId, tenantId);
                    break;
                case BusinessTypes.Transport:
                    result = await HandleTransportOrder(orderId, tenantId);
                    break;
                case BusinessTypes.None:
                default:
                    break;
            }
            if (result)
                await uom.Current.SaveChangesAsync();
        }

        async Task<bool> HandleTransportOrder(Guid orderId, string tenantId)
        {
            var order = await repository.GetAsync<TransportOrder>(orderId);
            if (order is null) return false;

            var orderInfo = new OrderDeliveryInfo
            {
                BusinessType = 1,
                CustomerId = order.Customer.CustomerId,
                OrderId = order.TransportId,
                FromAddressId = order.SenderInfo.AddressId,
                FromAddressName = order.SenderInfo.AddressName,
                FromProvince = order.SenderInfo.Province,
                FromCity = order.SenderInfo.City,
                FromArea = order.SenderInfo.District,
                ToAddressId = order.ReceiverInfo.AddressId,
                ToAddressName = order.ReceiverInfo.AddressName,
                ToProvince = order.ReceiverInfo.Province,
                ToCity = order.ReceiverInfo.City,
                ToArea = order.ReceiverInfo.District,
            };

            var result = await dataService.GetTransPlan(tenantId, orderInfo);
            if (!result.Success) return false;
            if (result.Data.Count == 1)
            {
                // matched only one transport strategy
                var transPlan = result.Data.First();
                SetTransResult(order, transPlan);

                return true;
            }

            return false;
        }

        async Task<bool> HandleInboundOrder(Guid orderId, string tenantId)
        {
            var order = await repository.GetAsync<InboundOrder>(orderId);
            if (order is null) return false;

            var warehouseAddr = await dataService.GetWarehouseAddress(tenantId, order.CargoOwner.CargoOwnerId, order.Warehouse);
            var orderInfo = new OrderDeliveryInfo
            {
                BusinessType = 1,
                CustomerId = order.Customer.CustomerId,
                OrderId = order.InboundId,
                FromAddressId = order.DeliveryInfo.AddressId,
                FromAddressName = order.DeliveryInfo.AddressName,
                FromProvince = order.DeliveryInfo.Province,
                FromCity = order.DeliveryInfo.City,
                FromArea = order.DeliveryInfo.District,
                ToAddressId = warehouseAddr.AddressId,
                ToAddressName = warehouseAddr.AddressName,
                ToProvince = warehouseAddr.Province,
                ToCity = warehouseAddr.City,
                ToArea = warehouseAddr.District,
            };

            var result = await dataService.GetTransPlan(tenantId, orderInfo);
            if (!result.Success) return false;
            if (result.Data.Count == 1)
            {
                // matched only one transport strategy
                var transPlan = result.Data.First();
                SetTransResult(order, transPlan);

                return true;
            }

            return false;
        }

        async Task<bool> HandleOutboundOrder(Guid orderId, string tenantId)
        {
            var order = await repository.GetAsync<OutboundOrder>(orderId);
            if (order is null) return false;

            var warehouseAddr = await dataService.GetWarehouseAddress(tenantId, order.CargoOwner.CargoOwnerId, order.Warehouse);
            var orderInfo = new OrderDeliveryInfo
            {
                BusinessType = 1,
                CustomerId = order.Customer.CustomerId,
                OrderId = order.OutboundId,
                FromAddressId = warehouseAddr.AddressId,
                FromAddressName = warehouseAddr.AddressName,
                FromProvince = warehouseAddr.Province,
                FromCity = warehouseAddr.City,
                FromArea = warehouseAddr.District,
                ToAddressId = order.DeliveryInfo.AddressId,
                ToAddressName = order.DeliveryInfo.AddressName,
                ToProvince = order.DeliveryInfo.Province,
                ToCity = order.DeliveryInfo.City,
                ToArea = order.DeliveryInfo.District
            };

            var result = await dataService.GetTransPlan(tenantId, orderInfo);
            if (!result.Success) return false;
            if (result.Data.Count == 1)
            {
                // matched only one transport strategy
                var transPlan = result.Data.First();
                SetTransResult(order, transPlan);

                return true;
            }

            return false;
        }

        void SetTransResult(BusinessOrder order, TransportPlanRet transPlan)
        {
            List<TransportResource> resources = new List<TransportResource>();
            int index = 0;
            foreach (var item in transPlan.TransPlanDetails.OrderBy(d => d.Sort))
            {
                resources.Add(new TransportResource
                {
                    Index = index++,
                    ResourceId = item.LabelKey,
                    Name = item.Label
                });
                resources.Add(new TransportResource
                {
                    Index = index++,
                    ResourceId = item.TipKey,
                    Name = item.TipKey
                });
            }

            var strategy = new TransportStrategy(transPlan.PlanName, transPlan.Remark, 1, resources);
            order.SetMatchedResult(strategy);
        }
    }
}
