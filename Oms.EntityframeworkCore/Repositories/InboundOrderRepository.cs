using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oms.Domain.InboundOrders;
using Oms.Domain.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace Oms.EntityframeworkCore.Repositories
{
    public class InboundOrderRepository : EfCoreRepository<OmsDbContext, InboundOrder, Guid>, IInboundOrderRepository
    {
        public InboundOrderRepository(IDbContextProvider<OmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public async Task<InboundOrder?> InsertOrderAsync(InboundOrder newInboundOrder)
        {
            if (newInboundOrder == null)
                throw new ArgumentNullException(nameof(newInboundOrder));

            var context = await GetDbContextAsync();
            _ = await context.Set<InboundOrder>().AddAsync(newInboundOrder);
            await context.SaveChangesAsync();
            return newInboundOrder;
        }

        public async Task<bool> UpdateAsync(InboundOrder newInboundOrder)
        {
            if (newInboundOrder == null || newInboundOrder.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(newInboundOrder));
            
            var context = await GetDbContextAsync();
            var entry = context.Set<InboundOrder>().Update(newInboundOrder);

            try
            {
                if (await context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<InboundOrder?> GetAsync(Guid id)
        {
            var context = await GetDbContextAsync();
            return await context.Set<InboundOrder>().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<InboundOrder>> GetAsync(IEnumerable<Guid> ids)
        {
            if (ids is null || ids.Any())
                throw new ArgumentNullException(nameof(ids));

            var context = await GetDbContextAsync();
            return await context.Set<InboundOrder>().Where(o => ids.Contains(o.Id)).ToListAsync() ?? new List<InboundOrder>();
        }

        public async Task<InboundOrder?> GetByOrderNumberAsync(string orderNumber)
        {
            var context = await GetDbContextAsync();
            return await context.Set<InboundOrder>().SingleOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<InboundOrder?> GetByIdAsync(long inboundId)
        {
            var context = await GetDbContextAsync();
            return await context.Set<InboundOrder>().SingleOrDefaultAsync(o => o.InboundId == inboundId);
        }

        public async Task<IEnumerable<InboundOrder>> ListAsync(Specification<InboundOrder> specification)
        {
            var context = GetDbContextAsync().Result;

            return await context.Set<InboundOrder>()
                .AsQueryable()
                .Where(specification.ToExpression())
                .ToListAsync();
        }
    }
}
