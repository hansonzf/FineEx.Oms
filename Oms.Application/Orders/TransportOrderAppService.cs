using Microsoft.Extensions.Logging;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Contracts.TransportOrders;
using Oms.Application.Jobs;
using Oms.Domain.Orders;
using Oms.Domain.OutboundOrders;
using Oms.Domain.Shared.Orders.Events;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;
using Volo.Abp.ObjectMapping;

namespace Oms.Application.Orders
{
    public class TransportOrderAppService : ApplicationService, ITransportOrderAppService
    {
        private readonly ITransportOrderRepository repository;
        private readonly IJobManager jobManager;
        private readonly ILogger<TransportOrderAppService> logger;
        private readonly IThreePLService dataService;
        readonly ILocalEventBus _localEventBus;

        public TransportOrderAppService(
            ITransportOrderRepository repository,
            IJobManager jobManager,
            ILogger<TransportOrderAppService> logger,
            IThreePLService dataService,
            ILocalEventBus localEventBus)
        {
            this.repository = repository;
            this.jobManager = jobManager;
            this.logger = logger;
            this.dataService = dataService;
            _localEventBus = localEventBus;
        }

        public async Task<DataResult<TransportOrderDto>> GetAsync(Guid orderId)
        {
            var order = await repository.GetAsync(orderId);
            if (order is null)
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = null,
                    Message = $"The order {orderId} is not found"
                };
            else
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = ObjectMapper.Map<TransportOrder, TransportOrderDto>(order),
                    Message = "OK"
                };
        }

        public async Task<DataResult<TransportOrderDto>> GetAsync(string orderNumber)
        {
            var order = await repository.GetAsync(orderNumber);
            if (order is null)
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = null,
                    Message = $"The order {orderNumber} is not found"
                };
            else
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = ObjectMapper.Map<TransportOrder, TransportOrderDto>(order),
                    Message = "OK"
                };
        }

        public async Task<DataResult<TransportOrderDto>> GetAsync(long transportId)
        {
            var order = await repository.GetAsync(transportId);
            if (order is null)
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = null,
                    Message = $"The order which transportid {transportId} is not found"
                };
            else
                return new DataResult<TransportOrderDto>
                {
                    Success = true,
                    Data = ObjectMapper.Map<TransportOrder, TransportOrderDto>(order),
                    Message = "OK"
                };
        }

        public async Task<TransportOrderDto?> CreateOrderAsync(TransportOrderDto orderDto)
        {
            Guid orderId = GuidGenerator.Create();
            orderDto.Details.ForEach(d => d.BindToOrder(orderId));
            long transportId = DateTime.Now.Ticks; //IdGenerator.GetId();
            string orderNumber = DateTime.Now.ToString("TRyyyyMMddhhmmssffff");

            var order = new TransportOrder(orderId, transportId, orderNumber, orderDto.TenantId, orderDto.ExternalOrderNumber, orderDto.IsReturnBack, orderDto.TransportType, orderDto.Customer, orderDto.Sender, orderDto.Receiver, orderDto.ExpectingPickupTime, orderDto.ExpectingCompleteTime, orderDto.Remark);
            order.AddOrRepalceCargos(orderDto.Details);
            var createdOrder = await repository.InsertAsync(order);
            if (createdOrder != null)
                return ObjectMapper.Map<TransportOrder, TransportOrderDto>(createdOrder);
            else
                return null;
        }

        public async Task<bool> UpdateOrderAsync(TransportOrderDto orderDto)
        {
            if (orderDto.Id == Guid.Empty)
                return false;

            var order = await repository.GetAsync(orderDto.Id);
            if (order is null)
                return false;

            order.UpdateInformation(
                orderDto.TransportType,
                orderDto.Remark,
                orderDto.ExpectingPickupTime,
                orderDto.ExpectingCompleteTime,
                orderDto.Customer,
                orderDto.Sender,
                orderDto.Receiver);

            return await repository.UpdateAsync(order);
        }

        public async Task<TransportOrderDto> GetDetailAsync(Guid orderId)
        {
            //var order = await repository.GetAsync(orderId);
            //if (order is null)
            //    return new DataResult<TransportOrderDto>
            //    {
            //        Success = true,
            //        Data = null,
            //        Message = $"The order {orderId} is not found"
            //    };
            //var dto = ObjectMapper.Map<TransportOrder, TransportOrderDto>(order);
            //var locations = await dataService.GetAddress(new CurrentUser { TenantId = order.TenantId });
            



            //if (order is null)
            //    return new DataResult<TransportOrderDto>
            //    {
            //        Success = true,
            //        Data = null,
            //        Message = $"The order {orderId} is not found"
            //    };
            //else
            //    return new DataResult<TransportOrderDto>
            //    {
            //        Success = true,
            //        Data = ObjectMapper.Map<TransportOrder, TransportOrderDto>(order),
            //        Message = "OK"
            //    };
            throw new NotImplementedException();
        }

        public async Task<ListResult<TransportOrderDto>> ListOrderAsync(TransportQueryDto query)
        {
            var spec = new TransportQuerySpecification
            {
                TimeRange = query.TimeRange,
                TimeType = query.TimeType,
                //CustomerId = query.CustomerId,
                FromAddressName = query.FromAddressName,
                ToAddressName = query.ToAddressName,
                IsReturnBack = query.IsBack,
                OrderState = query.OrderStatus,
                TransportType = query.OrderType
            };
            var orders = await repository.ListAsync(spec);
            var dtos = ObjectMapper.Map<IEnumerable<TransportOrder>, IEnumerable<TransportOrderDto>>(orders);

            return new ListResult<TransportOrderDto>
            {
                Items = dtos,
                Success = true
            };
        }
    }
}
