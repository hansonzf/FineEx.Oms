using Oms.Domain.Orders;
using Oms.Domain.Shared.Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.UndoDispatchEventHandlers
{
    internal class UndoIssueToTmsHandler
        : ILocalEventHandler<UndoDispatchOrdersEvent>, ITransientDependency
    {
        public Task HandleEventAsync(UndoDispatchOrdersEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
