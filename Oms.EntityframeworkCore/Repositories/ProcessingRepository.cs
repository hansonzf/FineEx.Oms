using Microsoft.EntityFrameworkCore;
using Oms.Domain.Processings;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Oms.EntityframeworkCore.Repositories
{
    public class ProcessingRepository : EfCoreRepository<OmsDbContext, Processing, Guid>, IProcessingRepository
    {
        public ProcessingRepository(IDbContextProvider<OmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public async Task<bool> InsertProcessingAsync(Processing processing)
        {
            var context = await GetDbContextAsync();
            var entry = await context.Set<Processing>().AddAsync(processing);
            return await context.SaveChangesAsync() == 1;
        }

        public async Task<Processing?> GetAsync(Guid id)
        {
            var context = await GetDbContextAsync();
            return await context.Set<Processing>().FindAsync(id);
        }

        public async Task<Processing?> GetByOrderIdAsync(Guid orderId)
        {
            var context = await GetDbContextAsync();
            return await context.Set<Processing>().FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<IEnumerable<Processing>> GetWaitforBuildProcessing(int count = 50)
        {
            var context = await GetDbContextAsync();
            int maxExecutedCount = 5;

            var processings = await context.Set<Processing>()
                .Where(p => !p.IsScheduled && p.Processed != (int)p.Steps && p.ExecutedCount <= maxExecutedCount)
                .Take(count).ToListAsync();

            return processings;
        }
    }
}
