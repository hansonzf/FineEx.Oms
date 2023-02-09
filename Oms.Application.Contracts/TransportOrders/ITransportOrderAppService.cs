using Oms.Application.Contracts;
using Oms.Application.Contracts.TransportOrders;
using Volo.Abp.Application.Services;

namespace Oms.Application.Orders
{
    public interface ITransportOrderAppService : IApplicationService
    {
        Task<DataResult<TransportOrderDto>> GetAsync(Guid orderId);
        Task<TransportOrderDto> GetDetailAsync(Guid orderId);
        Task<DataResult<TransportOrderDto>> GetAsync(string orderNumber);
        Task<DataResult<TransportOrderDto>> GetAsync(long transportId);
        Task<TransportOrderDto?> CreateOrderAsync(TransportOrderDto orderDto);
        Task<bool> UpdateOrderAsync(TransportOrderDto orderDto);
        Task<ListResult<TransportOrderDto>> ListOrderAsync(TransportQueryDto query);
    }
}
