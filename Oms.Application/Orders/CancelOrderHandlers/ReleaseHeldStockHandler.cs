using Oms.Application.Contracts.CollaborationServices.Inventory;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.CancelOrderHandlers
{
    public class ReleaseHeldStockHandler
        : ILocalEventHandler<CancelOrderEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IInventoryService inventoryService;

        public ReleaseHeldStockHandler(IOrderRepository repository, IInventoryService inventoryService)
        {
            this.repository = repository;
            this.inventoryService = inventoryService;
        }

        public async Task HandleEventAsync(CancelOrderEvent eventData)
        {
            //if (eventData.BusinessType == BusinessTypes.OutboundWithTransport)
            //{
            //    if ((int)eventData.OrderState < 2) return;

            //    var order = await repository.GetAsync<OutboundOrder>(eventData.OrderId) as OutboundOrder;
            //    if (order is null) return;

            //    await inventoryService.StockRelease(
            //        order.Warehouse.WarehouseId.ToString(),
            //        new SaleInventoryFreeRequest
            //        {
            //            BusinessType = 2,
            //            OrderList = new List<OrderModel>
            //            {
            //                new OrderModel
            //                {
            //                    OutBoundID = order.OutboundId,
            //                    MemberID = order.CargoOwner.CargoOwnerId,
            //                    WarehouseID = order.Warehouse.WarehouseId,
            //                    OperationType = 2
            //                }
            //            }
            //        });
            //}
        }
    }
}
