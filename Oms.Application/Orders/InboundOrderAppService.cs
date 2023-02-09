using Oms.Application.Contracts;
using Oms.Application.Contracts.InboundOrders;
using Oms.Domain.InboundOrders;
using Oms.Domain.Orders;
using Oms.Domain.OutboundOrders;
using Volo.Abp.Application.Services;

namespace Oms.Application.Orders
{
    public class InboundOrderAppService : ApplicationService, IInboundOrderAppService
    {
        private readonly IInboundOrderRepository repository;

        public InboundOrderAppService(IInboundOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> CreateInboundOrderAsync(InboundOrderDto orderDto)
        {
            Guid orderId = GuidGenerator.Create();
            orderDto.Details.ForEach(d => d.BindToOrder(orderId));
            long inboundId = DateTime.Now.Ticks; //IdGenerator.GetId();
            string orderNumber = DateTime.Now.ToString("STyyyyMMddhhmmssffff");
            var order = new InboundOrder(
                orderId,
                inboundId,
                orderDto.TenantId,
                orderNumber,
                orderDto.ExternalOrderNumber,
                orderDto.Customer,
                orderDto.CargoOwner,
                orderDto.DeliveryInfo,
                orderDto.Warehouse,
                orderDto.IsReturnOrder,
                orderDto.OriginDeliveryNumber,
                orderDto.InboundType,
                orderDto.ExpectingCompleteTime,
                orderDto.Remark);
            order.AddOrRepalceProducts(orderDto.Details);
            var createdOrder = await repository.InsertOrderAsync(order);
            
            return createdOrder != null;
        }

        public async Task<InboundOrderDto?> GetOrderByOrderNumberAsync(string orderNumber)
        {
            var order = await repository.GetByOrderNumberAsync(orderNumber);
            if (order is not null)
                return ObjectMapper.Map<InboundOrder, InboundOrderDto>(order);
            else
                return null;
        }

        public async Task<InboundOrderDto?> GetOrderByIdAsync(long inboundId)
        {
            var order = await repository.GetByIdAsync(inboundId);
            if (order is not null)
                return ObjectMapper.Map<InboundOrder, InboundOrderDto>(order);
            else
                return null;
        }

        public async Task<ListResult<InboundOrderDto>> ListOrderAsync(InboundQueryDto query)
        {
            var spec = new InboundQuerySpecification
            {
                
            };
            var orders = await repository.ListAsync(spec);
            var dtos = ObjectMapper.Map<IEnumerable<InboundOrder>, IEnumerable<InboundOrderDto>>(orders);

            return new ListResult<InboundOrderDto>
            {
                Items = dtos,
                Success = true
            };
        }

        public async Task<bool> UpdateOrderAsync(InboundOrderDto orderDto)
        {
            var order = await repository.GetByIdAsync(orderDto.InboundId);
            if (order is null) return false;
            order.Update(
                orderDto.ExpectingCompleteTime, 
                orderDto.IsReturnOrder, 
                orderDto.Remark, 
                orderDto.OriginDeliveryNumber,
                orderDto.Customer, 
                orderDto.CargoOwner, 
                orderDto.DeliveryInfo, 
                orderDto.Warehouse);

            return await repository.UpdateAsync(order);
        }

    }
}
