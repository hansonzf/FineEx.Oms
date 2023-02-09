using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Oms.Domain.Orders;
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

            //var orderIds = await context.Set<OutboundOrder>()
            //    .Where(o => o.OrderState == OrderStatus.CheckingStock)
            //    .Select(o => o.Id).ToListAsync();

            var processings = await context.Set<Processing>()
                .Where(p => !p.IsScheduled && p.Processed != (int)p.Steps)
                .Take(count).ToListAsync();

            return processings;
        }
    }
}
