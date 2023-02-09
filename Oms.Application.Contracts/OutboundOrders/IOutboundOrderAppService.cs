using Oms.Application.Contracts;
using Volo.Abp.Application.Services;

namespace Oms.Application.Orders
{
    public interface IOutboundOrderAppService : IApplicationService
    {
        Task<ServiceResult> CreateOutboundOrderAsync(OutboundOrderDto orderDto);
        Task<DataResult<OutboundOrderDto>> GetOutboundOrderByIdAsync(Guid orderId);
        Task<DataResult<OutboundOrderDto>> GetOutboundOrderByIdAsync(long outboundId);
        Task<ServiceResult> CombineOrdersAsync(Guid[] orderIds);
        Task<ServiceResult> CombineOrdersAsync(long[] orderIds);
        Task<ServiceResult> UndoCombineAsync(long masterOrderId, long[] slaveOrderIds);
        Task<ServiceResult> UndoCombineAsync(Guid masterOrderId, Guid[] slaveOrderIds);
        Task<ListResult<CombineDetailDto>> GetCombineDetail(Guid masterOrderId);
        Task<ServiceResult> SetCheckStockResultAsync(CheckstockResultDto result);
        Task<ListResult<OutboundOrderDto>> ListOrdersAsync(OutboundQueryDto query);
    }
}
