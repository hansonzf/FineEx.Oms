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
        readonly IUnitOfWorkManager uom;
        readonly IJobManager jobManager;

        public CompleteProcessingHandler(IProcessingRepository repository, IUnitOfWorkManager uom, IJobManager jobManager)
        {
            this.repository = repository;
            this.uom = uom;
            this.jobManager = jobManager;
        }

        public async Task HandleEventAsync(JobExecutedEvent eventData)
        {
            var unitOfWork = uom.Begin();
            var processing = await repository.GetByOrderIdAsync(eventData.OrderId);
            if (processing is null) return;

            if (eventData.IsSuccess)
            {
                if (!processing.IsScheduled && processing.Job == null)
                    return;

                processing.TaskExecuteSuccess(eventData.Proc, true);
            }

            if (!eventData.IsSuccess && processing.ExecutedCount <= 5)
            {
                var job = processing.Job;
                await jobManager.CancelJobAsync(job.JobName, job.GroupName);
                processing.TaskExecuteFailed(eventData.Proc);
            }

            //await uom.Current.SaveChangesAsync();
            await unitOfWork.CompleteAsync();
        }
    }
}
