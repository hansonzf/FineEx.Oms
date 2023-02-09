using Oms.Domain.Orders;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace Oms.Domain.InboundOrders
{
    public interface IInboundOrderRepository : IRepository
    {
        Task<InboundOrder?> InsertOrderAsync(InboundOrder newInboundOrder);
        Task<bool> UpdateAsync(InboundOrder newInboundOrder);
        Task<InboundOrder?> GetByIdAsync(long inboundId);
        Task<InboundOrder?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<InboundOrder>> GetAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<InboundOrder>> ListAsync(Specification<InboundOrder> specification);
    }
}
