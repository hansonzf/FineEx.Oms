using Oms.Application.Jobs;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Processings.BuildTaskEventHandlers
{
    public class ScheduleJobForProcessingHandler
        : ILocalEventHandler<BuildingTaskEvent>, ITransientDependency
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProcessingRepository processingRepository;
        private readonly IJobManager jobManager;
        private readonly IUnitOfWorkManager uow;

        public ScheduleJobForProcessingHandler(
            IOrderRepository orderRepository,
            IJobManager jobManager,
            IProcessingRepository processingRepository,
            IUnitOfWorkManager uow)
        {
            this.orderRepository = orderRepository;
            this.jobManager = jobManager;
            this.processingRepository = processingRepository;
            this.uow = uow;
        }

        public async Task HandleEventAsync(BuildingTaskEvent eventData)
        {
            if (eventData == null)
                return;

            long? orderId = await orderRepository.GetOrderIdByOrderUuidAsync(eventData.OrderUuid, eventData.BusinessType);
            var parameters = new Dictionary<string, string>
            {
                { JobConstants.JobDataMapBusinessTypeKeyName, ((int)eventData.BusinessType).ToString() },
                { JobConstants.JobDataMapTenantIdKeyName, eventData.TenantId },
                { JobConstants.JobDataMapOrderIdKeyName, orderId.HasValue ? orderId.Value.ToString() : "0" },
                { JobConstants.JobDataMapProcessTypeKeyName, Enum.GetName(eventData.CurrentStep) }
            };
            if (eventData.BusinessType == BusinessTypes.OutboundWithTransport)
            {
                var relatedOrderIds = await orderRepository.GetRelatedOrderIds(eventData.OrderUuid, eventData.BusinessType);
                parameters.Add(JobConstants.JobDataMapRelatedOrderIdsOrderIdKeyName, relatedOrderIds);
            }

            var jobInfo = await jobManager.ScheduleAsync(eventData.OrderUuid, eventData.BusinessType, eventData.CurrentStep, parameters, eventData.DelayMillisecondsStart);
            var processing = await processingRepository.GetAsync(eventData.ProcessingId);
            processing.SetBuiltTaskResult(jobInfo.JobName, jobInfo.GroupName, jobInfo.TriggerName, jobInfo.TriggerGroup);
            await uow.Current.SaveChangesAsync();
        }
    }
}
