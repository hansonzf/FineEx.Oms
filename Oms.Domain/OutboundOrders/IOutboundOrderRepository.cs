using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace Oms.Domain.Orders
{
    public interface IOutboundOrderRepository : IRepository
    {
        Task<IEnumerable<OutboundOrder>> GetCombinedOrdersAsync(Guid masterOrderId);
        Task<IEnumerable<Guid>> GetOrderUuidByOrderIdAsync(long[] orderIds);
        Task<OutboundOrder?> GetAsync(Guid orderId);
        Task<OutboundOrder?> GetAsync(long outboundId);
        Task<IEnumerable<OutboundOrder>> GetByIdsAsync(Guid[] ids);
        Task<OutboundOrder?> CreateAsync(OutboundOrder order);
        Task<IEnumerable<OutboundOrder>> ListAsync(Specification<OutboundOrder> specification);
    }
}
