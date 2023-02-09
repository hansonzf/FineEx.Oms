using Oms.Application.Contracts;
using Oms.Application.Contracts.InboundOrders;
using Volo.Abp.Application.Services;

namespace Oms.Application.Orders
{
    public interface IInboundOrderAppService : IApplicationService
    {
        Task<bool> CreateInboundOrderAsync(InboundOrderDto orderDto);
        Task<InboundOrderDto?> GetOrderByOrderNumberAsync(string orderNumber);
        Task<InboundOrderDto?> GetOrderByIdAsync(long inboundId);
        Task<bool> UpdateOrderAsync(InboundOrderDto orderDto);
        Task<ListResult<InboundOrderDto>> ListOrderAsync(InboundQueryDto query);
    }
}
