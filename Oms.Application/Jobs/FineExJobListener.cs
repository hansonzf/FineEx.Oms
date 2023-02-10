using Oms.Domain.Jobs;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Quartz;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Uow;

namespace Oms.Application.Jobs
{
    public class FineExJobListener : IJobListener, IUnitOfWorkEnabled
    {
        private readonly ILocalEventBus eventBus;

        public FineExJobListener(ILocalEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public string Name => "FineEx-OMS-JobListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            try
            {
                var jobKey = context.JobDetail.Key;
                if (jobKey.Group == JobHelper.GlobalTriggerGroupName)
                    return;

                Guid orderId = Guid.Parse(jobKey.Name);
                BusinessTypes businessType = BusinessTypes.None;
                string processingType = context.JobDetail.JobDataMap.GetString(JobConstants.JobDataMapProcessTypeKeyName);
                string? bizTypeStr = context.JobDetail.JobDataMap.GetString(JobConstants.JobDataMapBusinessTypeKeyName);
                if (!string.IsNullOrEmpty(bizTypeStr))
                    businessType = (BusinessTypes)Enum.Parse(typeof(BusinessTypes), bizTypeStr);
                ProcessingSteps proc = Enum.Parse<ProcessingSteps>(processingType);
                var evt = new JobExecutedEvent
                {
                    OrderId = orderId,
                    Proc = proc,
                    BusinessType = businessType,
                    IsSuccess = true
                };

                if (jobException is not null)
                    evt.IsSuccess = false;

                await eventBus.PublishAsync<JobExecutedEvent>(evt);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
