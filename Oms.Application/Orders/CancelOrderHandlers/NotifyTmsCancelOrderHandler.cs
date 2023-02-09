using Oms.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.CancelOrderHandlers
{
    public class NotifyTmsCancelOrderHandler
        : ILocalEventHandler<CancelOrderEvent>, ITransientDependency
    {
        public Task HandleEventAsync(CancelOrderEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
