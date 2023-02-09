using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace Oms.Domain.Orders
{
    public interface ITransportOrderRepository : IRepository
    {
        Task<TransportOrder?> InsertAsync(TransportOrder newTransportOrder);
        Task<bool> UpdateAsync(TransportOrder transportOrder);
        Task<TransportOrder?> GetAsync(Guid id);
        Task<IEnumerable<TransportOrder>> GetOrdersAsync(IEnumerable<long> ids);
        Task<IEnumerable<TransportOrder>> GetAsync(IEnumerable<Guid> ids);
        Task<TransportOrder?> GetAsync(string orderNumber);
        Task<TransportOrder?> GetAsync(long transportId);
        Task<IEnumerable<TransportOrder>> ListAsync(Specification<TransportOrder> specification);
    }
}
