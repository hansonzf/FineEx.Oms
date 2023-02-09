using Volo.Abp.Domain.Repositories;

namespace Oms.Domain.Processings
{
    public interface IProcessingRepository : IRepository
    {
        Task<Processing?> GetAsync(Guid id);
        Task<Processing?> GetByOrderIdAsync(Guid orderId);
        Task<bool> InsertProcessingAsync(Processing processing);
        Task<IEnumerable<Processing>> GetWaitforBuildProcessing(int count = 50);
    }
}
