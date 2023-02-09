using Oms.Application.Jobs;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Oms.Application.Processings.StartRunTaskEventHandlers
{
    public class JobManagerStartTaskHandler
        : IDistributedEventHandler<StartRunTaskEvent>, ITransientDependency
    {
        private readonly IJobManager jobManager;
        private readonly IProcessingRepository repository;

        public JobManagerStartTaskHandler(IJobManager jobManager, IProcessingRepository repository)
        {
            this.jobManager = jobManager;
            this.repository = repository;
        }

        public async Task HandleEventAsync(StartRunTaskEvent eventData)
        {
            var processing = await repository.GetAsync(eventData.ProcessingId);
            if (processing is null || !processing.IsScheduled)
                return;

            var jobInfo = processing.Job;
            await jobManager.RunAsync(jobInfo.JobName, jobInfo.GroupName);
        }
    }
}
