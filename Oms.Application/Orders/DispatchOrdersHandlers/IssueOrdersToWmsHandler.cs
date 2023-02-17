using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;

namespace Oms.Application.Orders.DispatchOrdersHandlers
{
    internal class IssueOrdersToWmsHandler
        : ILocalEventHandler<DispatchOrdersEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IWmsService wmsService;
        readonly IObjectMapper<OmsApplicationModule> objectMapper;

        public IssueOrdersToWmsHandler(IOrderRepository repository, IWmsService wmsService, IObjectMapper<OmsApplicationModule> objectMapper)
        {
            this.repository = repository;
            this.wmsService = wmsService;
            this.objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(DispatchOrdersEvent eventData)
        {
            var orders = new List<BusinessOrder>();
            if (eventData.OrderId.HasValue)
            {
                var id = await repository.GetOrderIdByOrderUuidAsync(eventData.OrderId.Value, eventData.BusinessType);
                if (!id.HasValue) return;
                var o = await repository.GetOrderByIdAsync(id.Value, eventData.BusinessType);
                if (o == null) return;
                orders.Add(o);
            }
            else if (eventData.OrderIds.Any())
            {
                var o = await repository.GetOrdersByBusinessType(eventData.OrderIds, eventData.BusinessType);
                if (o.Any())
                    orders.AddRange(o);
            }

            var orderDictionary = GroupOrdersByWarehouse(eventData.BusinessType, orders);

            if (eventData.BusinessType == BusinessTypes.InboundWithTransport)
            {
                foreach (var item in orderDictionary)
                {
                    await DispatchInboundOrders(item.Key, item.Value.OfType<InboundOrder>());
                }
            }

            if (eventData.BusinessType == BusinessTypes.OutboundWithTransport)
            {
                foreach (var item in orderDictionary)
                {
                    await DispatchOutboundOrders(item.Key, item.Value.OfType<OutboundOrder>());
                }
            }
        }

        Dictionary<int, IEnumerable<BusinessOrder>> GroupOrdersByWarehouse(BusinessTypes businessType, IEnumerable<BusinessOrder> orders)
        {
            var result = new Dictionary<int, IEnumerable<BusinessOrder>>();

            if (businessType == BusinessTypes.InboundWithTransport)
            {
                var inboundGroup = orders.OfType<InboundOrder>().GroupBy(o => o.Warehouse);
                foreach ( var group in inboundGroup)
                {
                    result.Add(group.Key.WarehouseId, group);
                }
            }

            if (businessType == BusinessTypes.OutboundWithTransport)
            {
                var outboundGroup = orders.OfType<OutboundOrder>().GroupBy(o => o.Warehouse);
                foreach (var group in outboundGroup)
                {
                    result.Add(group.Key.WarehouseId, group);
                }
            }

            return result;
        }

        async Task DispatchOutboundOrders(int warehouseId, IEnumerable<OutboundOrder> outboundOrders)
        {
            var orders = objectMapper.Map<IEnumerable<OutboundOrder>, IEnumerable<OutboundOrderDto>>(outboundOrders);
            await wmsService.DispatchOutboundOrdersAsync(warehouseId.ToString(), orders);
        }

        async Task DispatchInboundOrders(int warehouseId, IEnumerable<InboundOrder> inboundOrders)
        {
            var orders = objectMapper.Map<IEnumerable<InboundOrder>, IEnumerable<InboundOrderDto>>(inboundOrders);
            await wmsService.DispatchOutboundOrdersAsync(warehouseId.ToString(), orders);
        }
    }
}
