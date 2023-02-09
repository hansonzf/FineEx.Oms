using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Contracts.CollaborationServices.Tms;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;

namespace Oms.Application.Orders.DispatchOrdersHandlers
{
    internal class IssueOrdersToTmsHandler
        : ILocalEventHandler<DispatchOrdersEvent>, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly ITmsService tmsService;
        readonly IThreePLService dataService;
        readonly IObjectMapper<OmsApplicationModule> objectMapper;

        public IssueOrdersToTmsHandler(IOrderRepository repository, ITmsService tmsService, IThreePLService dataService, IObjectMapper<OmsApplicationModule> objectMapper)
        {
            this.repository = repository;
            this.tmsService = tmsService;
            this.dataService = dataService;
            this.objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(DispatchOrdersEvent eventData)
        {
            var orders = await repository.GetOrdersByBusinessType(eventData.OrderIds, eventData.BusinessType);
            var ordersDto = new List<BusinessOrderDto>();
            var addressList = await dataService.GetAddress(eventData.TenantId);
            switch (eventData.BusinessType)
            {
                case BusinessTypes.OutboundWithTransport:
                    var outboundOrders = objectMapper.Map<IEnumerable<OutboundOrder>, IEnumerable<OutboundOrderDto>>(orders.OfType<OutboundOrder>());
                    ordersDto = outboundOrders.OfType<BusinessOrderDto>().ToList();
                    await tmsService.DispatchOrdersAsync(BusinessTypes.OutboundWithTransport, ordersDto, addressList);
                    break;
                case BusinessTypes.InboundWithTransport:
                    var inboundOrders = objectMapper.Map<IEnumerable<InboundOrder>, IEnumerable<InboundOrderDto>>(orders.OfType<InboundOrder>());
                    ordersDto = inboundOrders.OfType<BusinessOrderDto>().ToList();
                    await tmsService.DispatchOrdersAsync(BusinessTypes.InboundWithTransport, ordersDto, addressList);
                    break;
                case BusinessTypes.Transport:
                    var transportOrders = objectMapper.Map<IEnumerable<TransportOrder>, IEnumerable<TransportOrderDto>>(orders.OfType<TransportOrder>());
                    ordersDto = transportOrders.OfType<BusinessOrderDto>().ToList();
                    await tmsService.DispatchOrdersAsync(BusinessTypes.Transport, ordersDto, addressList);
                    break;
                case BusinessTypes.None:
                default:
                    break;
            }

        }
    }
}
