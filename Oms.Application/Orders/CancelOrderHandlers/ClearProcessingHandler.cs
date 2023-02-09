using Oms.Application.Jobs;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.CancelOrderHandlers
{
    public class ClearProcessingHandler
        : ILocalEventHandler<CancelOrderEvent>, ITransientDependency
    {
        readonly IJobManager jobManager;
        readonly IProcessingRepository repository;

        public ClearProcessingHandler(IJobManager jobManager, IProcessingRepository repository)
        {
            this.jobManager = jobManager;
            this.repository = repository;
        }

        public async Task HandleEventAsync(CancelOrderEvent eventData)
        {
            var processing = await repository.GetByOrderIdAsync(eventData.OrderId);
            if (processing is null)
                return;

            if (processing.Job is not null)
            {
                if (await jobManager.CancelJobAsync(processing.Job.JobName, processing.Job.GroupName))
                    processing.CancelJob();
            }
        }
    }
}
