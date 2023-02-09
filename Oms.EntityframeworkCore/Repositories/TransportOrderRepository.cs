using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oms.Domain.Orders;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace Oms.EntityframeworkCore.Repositories
{
    public class TransportOrderRepository : EfCoreRepository<OmsDbContext, TransportOrder, Guid>, ITransportOrderRepository
    {
        private readonly ILogger<TransportOrderRepository> logger;
        public TransportOrderRepository(
            IDbContextProvider<OmsDbContext> dbContextProvider, 
            ILogger<TransportOrderRepository> logger)
            : base(dbContextProvider)
        {
            this.logger = logger;
        }

        public async Task<TransportOrder?> GetAsync(Guid id)
        {
            var context = await GetDbContextAsync();
            return await context.Set<TransportOrder>().Include(o => o.Details).SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<TransportOrder?> GetAsync(string orderNumber)
        {
            var context = await GetDbContextAsync();
            return await context.Set<TransportOrder>().SingleOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<TransportOrder>> GetAsync(IEnumerable<Guid> ids)
        {
            if (ids is null || ids.Any())
                throw new ArgumentNullException(nameof(ids));

            var context = await GetDbContextAsync();
            return await context.Set<TransportOrder>().Where(o => ids.Contains(o.Id)).ToListAsync() ?? new List<TransportOrder>();
        }

        public async Task<TransportOrder?> GetAsync(long transportId)
        {
            var context = await GetDbContextAsync();
            return await context.Set<TransportOrder>().SingleOrDefaultAsync(o => o.TransportId == transportId);

        }

        public async Task<IEnumerable<TransportOrder>> GetOrdersAsync(IEnumerable<long> ids)
        {
            if (ids is null || ids.Any())
                throw new ArgumentNullException(nameof(ids));

            var context = await GetDbContextAsync();
            return await context.Set<TransportOrder>().Where(o => ids.Contains(o.TransportId)).ToListAsync() ?? new List<TransportOrder>();
        }

        public async Task<TransportOrder?> InsertAsync(TransportOrder newTransportOrder)
        {
            if (newTransportOrder == null)
                throw new ArgumentNullException(nameof(newTransportOrder));

            try
            {
                var context = await GetDbContextAsync();
                var entry = await context.Set<TransportOrder>().AddAsync(newTransportOrder);
                if (entry.State == EntityState.Added)
                {
                    await context.SaveChangesAsync();
                    return newTransportOrder;
                }
                else
                {
                    string orderData = JsonConvert.SerializeObject(newTransportOrder, Formatting.Indented);
                    logger.LogWarning("Add transport order to set failed!\r\nWhich data is: {orderData}", orderData);
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Save transport order into database occured error!");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(TransportOrder transportOrder)
        {
            if (transportOrder == null)
                return false;

            try
            {
                var context = await GetDbContextAsync();
                var entry = context.Set<TransportOrder>().Update(transportOrder);
                if (entry.State == EntityState.Modified)
                {
                    return await context.SaveChangesAsync() == 1;
                }
                else
                {
                    string orderData = JsonConvert.SerializeObject(transportOrder, Formatting.Indented);
                    logger.LogWarning("Update transport order to set failed!\r\nWhich data is: {orderData}", orderData);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Save transport order into database occured error!");
                return false;
            }
        }

        public async Task<IEnumerable<TransportOrder>> ListAsync(Specification<TransportOrder> specification)
        {
            var context = GetDbContextAsync().Result;

            return await context.Set<TransportOrder>()
                .AsQueryable()
                .Where(specification.ToExpression())
                .ToListAsync();
        }
    }
}
