using Oms.Application.Orders;

namespace Oms.Application.Contracts.CollaborationServices.Wms
{
    public interface IWmsService
    {
        Task<ServiceResult> DispatchOutboundOrdersAsync(string warehouseId, IEnumerable<BusinessOrderDto> orders);
        Task<ServiceResult> DispatchInboundOrdersAsync(string warehouseId, IEnumerable<BusinessOrderDto> orders);
        Task<ServiceResult> CancelOrder(long syncId, int memberId, int workerId, string cancelMemo);
    }
}
