using Oms.Domain.Orders;
using Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Oms.Application.Jobs
{
    public class CheckOutboundInventoryJob : IJob, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uom;

        public CheckOutboundInventoryJob(IOrderRepository repository, IUnitOfWorkManager uom)
        {
            this.repository = repository;
            this.uom = uom;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.MergedJobDataMap;
                string? orderUuid = dataMap.GetString(JobConstants.JobDataMapOrderIdKeyName);
                string? biz = dataMap.GetString(JobConstants.JobDataMapBusinessTypeKeyName);
                if (orderUuid == null || biz == null) return;
                long orderId = long.Parse(orderUuid);
                BusinessTypes businessType = biz switch
                { 
                    "1" => BusinessTypes.OutboundWithTransport,
                    "2" => BusinessTypes.InboundWithTransport,
                    "3" => BusinessTypes.Transport,
                    _ => BusinessTypes.None
                };
                var order = await repository.GetOrderByIdAsync(orderId, businessType);
                if (order == null) return;
                order.CheckInventory(true);
                await uom.Current.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(msg: ex.Message, refireImmediately: false, cause: ex);
            }
        }
    }
}
