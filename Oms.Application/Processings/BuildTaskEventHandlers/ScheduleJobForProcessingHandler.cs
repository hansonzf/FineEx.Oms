using Oms.Application.Jobs;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Oms.Application.Processings.BuildTaskEventHandlers
{
    public class ScheduleJobForProcessingHandler
        : ILocalEventHandler<BuildingTaskEvent>, ITransientDependency
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProcessingRepository processingRepository;
        private readonly IJobManager jobManager;
        private readonly IObjectMapper<OmsApplicationModule> mapper;
        private readonly IUnitOfWorkManager uow;

        public ScheduleJobForProcessingHandler(
            IOrderRepository orderRepository,
            IJobManager jobManager,
            IObjectMapper<OmsApplicationModule> mapper,
            IProcessingRepository processingRepository,
            IUnitOfWorkManager uow)
        {
            this.orderRepository = orderRepository;
            this.jobManager = jobManager;
            this.mapper = mapper;
            this.processingRepository = processingRepository;
            this.uow = uow;
        }

        public async Task HandleEventAsync(BuildingTaskEvent eventData)
        {
            if (eventData == null)
                return;

            BusinessOrderDto orderDto = new BusinessOrderDto();
            switch (eventData.BusinessType)
            {
                case BusinessTypes.OutboundWithTransport:
                    var outboundOrder = await orderRepository.GetAsync<OutboundOrder>(eventData.OrderId);
                    if (outboundOrder is not null && outboundOrder is OutboundOrder)
                        orderDto = mapper.Map<OutboundOrder, OutboundOrderDto>(outboundOrder as OutboundOrder);
                    break;
                case BusinessTypes.InboundWithTransport:
                    var inboundOrder = await orderRepository.GetAsync<InboundOrder>(eventData.OrderId);
                    if (inboundOrder is not null && inboundOrder is InboundOrder)
                        orderDto = mapper.Map<InboundOrder, InboundOrderDto>(inboundOrder as InboundOrder);
                    break;
                case BusinessTypes.Transport:
                    var transportOrder = await orderRepository.GetAsync<TransportOrder>(eventData.OrderId);
                    if (transportOrder is not null && transportOrder is TransportOrder)
                        orderDto = mapper.Map<TransportOrder, TransportOrderDto>(transportOrder as TransportOrder);
                    break;
                default:
                    break;
            }
            var jobInfo = await jobManager.ScheduleAsync(orderDto, eventData.CurrentStep, eventData.DelayStart);
            var processing = await processingRepository.GetAsync(eventData.ProcessingId);
            processing.SetBuiltTaskResult(jobInfo.JobName, jobInfo.GroupName, jobInfo.TriggerName);
            await uow.Current.SaveChangesAsync();
        }
    }
}
