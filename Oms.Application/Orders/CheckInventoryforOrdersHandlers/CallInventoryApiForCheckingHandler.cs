using Oms.Application.Contracts.CollaborationServices.Inventory;
using Oms.Application.Contracts.Processings;
using Oms.Domain.Orders;
using System.Collections;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;

namespace Oms.Application.Orders.CheckInventoryforOrdersHandlers
{
    public class CallInventoryApiForCheckingHandler
        : ILocalEventHandler<CheckInventoryforOrderEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IObjectMapper<OmsApplicationModule> objectMapper;
        readonly IInventoryService inventoryService;
        readonly IProcessingAppService processingService;

        public CallInventoryApiForCheckingHandler(
            IOrderRepository repository, 
            IObjectMapper<OmsApplicationModule> objectMapper, 
            IInventoryService inventoryService, 
            IProcessingAppService processingService)
        {
            this.repository = repository;
            this.objectMapper = objectMapper;
            this.inventoryService = inventoryService;
            this.processingService = processingService;
        }

        public async Task HandleEventAsync(CheckInventoryforOrderEvent eventData)
        {
            // 目前只有仓配一体出库订单需要审核库存
            if (eventData.BusinessType != BusinessTypes.OutboundWithTransport)
                return;

            var orderIds = new List<Guid>();
            if (eventData.OrderId.HasValue)
                orderIds.Add(eventData.OrderId.Value);
            else if (eventData.OrderIds.Any())
            {
                foreach (var id in eventData.OrderIds)
                {
                    var uuid = await repository.GetOrderUuidByOrderIdAsync(id, eventData.BusinessType);
                    if (uuid.HasValue)
                        orderIds.Add(uuid.Value);
                }
            }

            var orders = await GetOutboundOrdersAsync(orderIds);
            await ExecuteChecking(orders);
        }

        async Task ExecuteChecking(IEnumerable<OutboundOrder> outboundOrders)
        {
            foreach (var orderGroup in outboundOrders.GroupBy(o => o.Warehouse))
            {
                var orders = objectMapper.Map<IEnumerable<OutboundOrder>, IEnumerable<OutboundOrderDto>>(orderGroup);
                var result = await inventoryService.CheckStock(orderGroup.Key.WarehouseId.ToString(), orders);
                if (!result.Success)
                    throw new BusinessException(message: "库存审核失败");
            }
        }

        async Task<IEnumerable<OutboundOrder>> GetOutboundOrdersAsync(IEnumerable<Guid> orderUuids)
        {
            if (!orderUuids.Any())
                return Array.Empty<OutboundOrder>();

            var orders = new List<OutboundOrder>();
            foreach (var uuid in orderUuids)
            {
                var combinedOrders = await repository.GetWithCombinedOrdersAsync<OutboundOrder>(uuid);
                if (!combinedOrders.Any())
                    continue;

                orders.AddRange(combinedOrders.OfType<OutboundOrder>());
            }

            return orders;
        }
    }
}
