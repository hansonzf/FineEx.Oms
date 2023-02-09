using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oms.Domain.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace Oms.EntityframeworkCore.Repositories
{
    public class OutboundOrderRepository : EfCoreRepository<OmsDbContext, BusinessOrder, Guid>, IOutboundOrderRepository
    {
        private readonly ILogger<OutboundOrderRepository> logger;

        public OutboundOrderRepository(
            IDbContextProvider<OmsDbContext> dbContextProvider, 
            ILogger<OutboundOrderRepository> logger)
            : base(dbContextProvider)
        {
            this.logger = logger;
        }

        public async Task<OutboundOrder?> CreateAsync(OutboundOrder order)
        {
            var context = await GetDbContextAsync();
            try
            {
                _ = await context.Set<OutboundOrder>().AddAsync(order);
                await context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                logger.LogException(ex, LogLevel.Error);
                throw;
            }
        }

        public async Task<OutboundOrder?> GetAsync(Guid orderId)
        {
            var context = await GetDbContextAsync();
            return await context.Set<OutboundOrder>().Include(o => o.Details).SingleOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<OutboundOrder?> GetAsync(long outboundId)
        {
            var context = await GetDbContextAsync();
            return await context.Set<OutboundOrder>().SingleOrDefaultAsync(o => o.OutboundId == outboundId);
        }

        public async Task<IEnumerable<OutboundOrder>> GetByIdsAsync(Guid[] ids)
        {
            if (ids is null || !ids.Any())
                return Array.Empty<OutboundOrder>();

            var context = await GetDbContextAsync();
            return await context.Set<OutboundOrder>().Where(o => ids.Contains(o.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetOrderUuidByOrderIdAsync(long[] orderIds)
        {
            if (!orderIds.Any())
                return Array.Empty<Guid>();

            var context = await GetDbContextAsync();
            var ids = await context.Set<OutboundOrder>().Where(o => orderIds.Contains(o.OutboundId)).Select(o => o.Id).ToListAsync();

            return ids ?? new List<Guid>();
        }

        public async Task<IEnumerable<OutboundOrder>> GetCombinedOrdersAsync(Guid masterOrderId)
        {
            var context = await GetDbContextAsync();
            var masterOrder = await GetAsync(masterOrderId);
            if (masterOrder is null)
                return Array.Empty<OutboundOrder>();

            string idString = masterOrderId.ToString().ToLower();
            var orders = await context.Set<OutboundOrder>().Include(o => o.Details).Where(
                o => EF.Property<string>(o, "_relatedOrderIds") == idString &&
                o.RelationType == RelationTypes.CombinedSlave).ToListAsync() ?? new List<OutboundOrder>();
            orders.Insert(0, masterOrder);

            return orders;
        }

        public async Task<IEnumerable<OutboundOrder>> ListAsync(Specification<OutboundOrder> specification)
        {
            var context = GetDbContextAsync().Result;

            return await context.Set<OutboundOrder>()
                .AsQueryable()
                .Where(specification.ToExpression())
                .ToListAsync();
        }
    }
}
