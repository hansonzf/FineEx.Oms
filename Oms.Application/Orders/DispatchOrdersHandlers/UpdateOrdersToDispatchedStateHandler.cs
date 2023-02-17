using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Orders.DispatchOrdersHandlers
{
    internal class UpdateOrdersToDispatchedStateHandler
        : ILocalEventHandler<DispatchOrdersEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uow;

        public UpdateOrdersToDispatchedStateHandler(IOrderRepository repository, IUnitOfWorkManager uow)
        {
            this.repository = repository;
            this.uow = uow;
        }

        public async Task HandleEventAsync(DispatchOrdersEvent eventData)
        {
            return;
            var orders = await repository.GetOrdersByBusinessType(eventData.OrderIds, eventData.BusinessType);
            foreach (var order in orders)
            {
                order.Dispatched();
            }
            await uow.Current.SaveChangesAsync();
        }
    }
}
