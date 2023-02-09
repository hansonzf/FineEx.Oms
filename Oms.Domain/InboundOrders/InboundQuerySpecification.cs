using Oms.Domain.Orders;
using System.Linq.Expressions;

namespace Oms.Domain.OutboundOrders
{
    public class InboundQuerySpecification : Volo.Abp.Specifications.Specification<InboundOrder>
    {
        public int TimeType { get; set; }
        public List<DateTime> TimeRange { get; set; } = new List<DateTime>();
        public long CustomerId { get; set; }
        public long CargoOwnerId { get; set; }
        public string CustomerOrderCode { get; set; }
        public string OrderCode { get; set; }
        public string TransOrderCode { get; set; }
        public int OrderStatus { get; set; }
        public long InWarehouseId { get; set; }
        public string InWarehouseCode { get; set; }

        public override Expression<Func<InboundOrder, bool>> ToExpression()
        {
            Expression<Func<InboundOrder, bool>> predicate = o => true;

            if (TimeRange.Count == 2)
            {
                predicate = TimeType switch
                {
                    1 => predicate.And(o => o.ReceivedAt > TimeRange.First() && o.ReceivedAt <= TimeRange.Last()),
                    2 => predicate.And(o => o.ExpectingCompleteTime > TimeRange.First() && o.ExpectingCompleteTime <= TimeRange.Last()),
                    3 => predicate.And(o => o.FactCompleteTime > TimeRange.First() && o.FactCompleteTime <= TimeRange.Last()),
                    _ => predicate
                };
            }

            //if (!string.IsNullOrEmpty(CustomerId))
            //    predicate = predicate.And(o => o.Customer.CustomerId == CustomerId);
            if (CargoOwnerId > 0)
                predicate = predicate.And(o => o.CargoOwner.CargoOwnerId == CargoOwnerId);
            if (!string.IsNullOrEmpty(CustomerOrderCode))
                predicate = predicate.And(o => o.OrderNumber == CustomerOrderCode);
            if (!string.IsNullOrEmpty(OrderCode))
                predicate = predicate.And(o => o.OrderNumber == OrderCode);
            //if(!string.IsNullOrEmpty(TransOrderCode))
            //    predicate = predicate.And(o => o.OrderNumber == OrderCode);
            if (OrderStatus > 0)
                predicate = predicate.And(o => o.OrderState == (Orders.OrderStatus)OrderStatus);
            if (InWarehouseId > 0)
                predicate = predicate.And(o => o.Warehouse.WarehouseId == InWarehouseId);
            //if (InWarehouseCode > 0)
            //    predicate = predicate.And(o => o.DeliveryType == (DeliveryTypes)DeliveryType);

            return predicate;
        }
    }
}
