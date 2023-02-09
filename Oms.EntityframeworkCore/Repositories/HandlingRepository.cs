using Oms.Domain.Handlings;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Oms.EntityframeworkCore.Repositories
{
    public class HandlingRepository : EfCoreRepository<OmsDbContext, Handling, Guid>, IHandlingRepository
    {
        public HandlingRepository(IDbContextProvider<OmsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task InsertAsync(Handling log)
        {
            var context = await GetDbContextAsync();
            await context.Set<Handling>().AddAsync(log);
            await context.SaveChangesAsync();
        }
    }
}
