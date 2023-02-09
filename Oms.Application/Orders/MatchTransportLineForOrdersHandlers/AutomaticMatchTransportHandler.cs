using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.MatchTransportLineForOrdersHandlers
{
    internal class AutomaticMatchTransportHandler
        : ILocalEventHandler<MatchTransportLineForOrdersEvent>, ITransientDependency
    {
        readonly IThreePLService dataService;

        public AutomaticMatchTransportHandler(IThreePLService dataService)
        {
            this.dataService = dataService;
        }

        public Task HandleEventAsync(MatchTransportLineForOrdersEvent eventData)
        {
            if (eventData.OrderId.HasValue)
            {
                // 自动由job出发执行，单个执行，通过uuid查找
            }
            else
            {
                // 手动执行，可批量执行，通过数字id查找
            }
            throw new NotImplementedException();
        }
    }
}
