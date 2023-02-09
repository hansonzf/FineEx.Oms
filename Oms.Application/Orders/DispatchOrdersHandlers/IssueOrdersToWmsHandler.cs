using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;

namespace Oms.Application.Orders.DispatchOrdersHandlers
{
    internal class IssueOrdersToWmsHandler
        : ILocalEventHandler<DispatchOrdersEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IWmsService wmsService;
        readonly IObjectMapper<OmsApplicationModule> objectMapper;

        public IssueOrdersToWmsHandler(IOrderRepository repository, IWmsService wmsService, IObjectMapper<OmsApplicationModule> objectMapper)
        {
            this.repository = repository;
            this.wmsService = wmsService;
            this.objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(DispatchOrdersEvent eventData)
        {
            var orders = await repository.GetOrdersByBusinessType(eventData.OrderIds, eventData.BusinessType);
            string warehouseId = "";
            var ordersDto = new List<BusinessOrderDto>();
            switch (eventData.BusinessType)
            {
                case BusinessTypes.OutboundWithTransport:
                    var outboundOrders = objectMapper.Map<IEnumerable<OutboundOrder>, IEnumerable<OutboundOrderDto>>(orders.OfType<OutboundOrder>());
                    ordersDto = outboundOrders.OfType<BusinessOrderDto>().ToList();
                    await wmsService.DispatchOutboundOrdersAsync(warehouseId, ordersDto);
                    break;
                case BusinessTypes.InboundWithTransport:
                    var inboundOrders = objectMapper.Map<IEnumerable<InboundOrder>, IEnumerable<InboundOrderDto>>(orders.OfType<InboundOrder>());
                    ordersDto = inboundOrders.OfType<BusinessOrderDto>().ToList();
                    await wmsService.DispatchOutboundOrdersAsync(warehouseId, ordersDto);
                    break;
                case BusinessTypes.Transport:
                case BusinessTypes.None:
                default:
                    break;
            }
            
        }
    }
}
