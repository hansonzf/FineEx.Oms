using Oms.Application.Orders;

namespace Oms.Application.Contracts.CollaborationServices.Inventory
{
    public interface IInventoryService
    {
        Task<ServiceResult> CheckStock(string partitionId, IEnumerable<OutboundOrderDto> orders);
        Task<ServiceResult> ReleaseStock(string partitionId, IEnumerable<OutboundOrderDto> orders);
    }
}
