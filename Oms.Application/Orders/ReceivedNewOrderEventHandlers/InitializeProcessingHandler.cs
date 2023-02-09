using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Oms.Application.Orders.ReceivedNewOrderEventHandlers
{
    public class InitializeProcessingHandler
        : IDistributedEventHandler<ReceivedNewOrderEvent>, ITransientDependency
    {
        private readonly IProcessingRepository _repository;

        public InitializeProcessingHandler(IProcessingRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleEventAsync(ReceivedNewOrderEvent eventData)
        {
            var orderProcessing = new Processing(eventData.OrderId, eventData.BusinessType);
            await _repository.InsertProcessingAsync(orderProcessing);
        }
    }
}
