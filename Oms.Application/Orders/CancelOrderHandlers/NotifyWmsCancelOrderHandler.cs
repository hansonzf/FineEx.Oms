using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Domain.Orders;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Oms.Application.Orders.CancelOrderHandlers
{
    public class NotifyWmsCancelOrderHandler
        : ILocalEventHandler<CancelOrderEvent>, ITransientDependency
    {
        readonly IWmsService wmsService;

        public NotifyWmsCancelOrderHandler(IWmsService wmsService)
        {
            this.wmsService = wmsService;
        }

        public async Task HandleEventAsync(CancelOrderEvent eventData)
        {
            var resp = await wmsService.CancelOrder(
                eventData.OutboundId,
                eventData.MemberID,
                eventData.Operater,
                eventData.CancelMemo
                );

            if (resp == null || !resp.Success)
                throw new BusinessException(message: "WMS取消订单失败");
        }
    }
}
