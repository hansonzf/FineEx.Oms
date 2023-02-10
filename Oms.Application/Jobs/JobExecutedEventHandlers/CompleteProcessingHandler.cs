using Microsoft.Extensions.Logging;
using Oms.Domain.Jobs;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Jobs.JobExecutedEventHandlers
{
    public class CompleteProcessingHandler
        : ILocalEventHandler<JobExecutedEvent>, ITransientDependency
    {
        readonly IProcessingRepository repository;
        readonly ILogger<CompleteProcessingHandler> logger;
        readonly IUnitOfWorkManager uom;

        public CompleteProcessingHandler(IProcessingRepository repository, ILogger<CompleteProcessingHandler> logger, IUnitOfWorkManager uom)
        {
            this.repository = repository;
            this.logger = logger;
            this.uom = uom;
        }

        public async Task HandleEventAsync(JobExecutedEvent eventData)
        {
            var processing = await repository.GetByOrderIdAsync(eventData.OrderId);
            if (processing is null)
            {
                logger.LogError("Can not find processing entity with {orderId} orderId", eventData.OrderId);
                return;
            }

            if (eventData.IsSuccess)
                processing.TaskExecuteSuccess(eventData.Proc);

            if (!eventData.IsSuccess && processing.ExecutedCount <= 5)
            {
                var job = processing.Job;
                processing.TaskExecuteFailed(eventData.Proc);
                processing.SetBuiltTaskResult(job.JobName, job.GroupName, job.TriggerName, job.TriggerGroup);
            }

            await uom.Current.SaveChangesAsync();
        }
    }
}
