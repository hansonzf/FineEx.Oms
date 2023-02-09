using Oms.Domain.Orders;
using System.Linq.Expressions;

namespace Oms.Domain.OutboundOrders
{
    public class TransportQuerySpecification : Volo.Abp.Specifications.Specification<TransportOrder>
    {
        public int TimeType { get; set; }
        public List<DateTime> TimeRange { get; set; } = new List<DateTime>();
        public int CustomerId { get; set; }
        public string FromAddressName { get; set; }
        public string ToAddressName { get; set; }
        public List<string> CustomerOrderCodes { get; set; }
        public List<string> OrderCodes { get; set; }
        public int OrderState { get; set; }
        public int IsReturnBack { get; set; }
        public int TransportType { get; set; }
        

        public override Expression<Func<TransportOrder, bool>> ToExpression()
        {
            Expression<Func<TransportOrder, bool>> predicate = o => true;

            if (TimeRange.Count == 2)
            {
                predicate = TimeType switch
                {
                    1 => predicate.And(o => o.ReceivedAt > TimeRange.First() && o.ReceivedAt <= TimeRange.Last()),
                    2 => predicate.And(o => o.FactPickupTime > TimeRange.First() && o.FactPickupTime <= TimeRange.Last()),
                    3 => predicate.And(o => o.FactCompleteTime > TimeRange.First() && o.FactCompleteTime <= TimeRange.Last()),
                    _ => predicate
                };
            }

            //if (!string.IsNullOrEmpty(CustomerId))
            //    predicate = predicate.And(o => o.Customer.CustomerId == CustomerId);
            //if (CargoOwnerId > 0)
            //    predicate = predicate.And(o => o.CargoOwner.CargoOwnerId == CargoOwnerId);
            //if (!string.IsNullOrEmpty(CustomerOrderCode))
            //    predicate = predicate.And(o => o.OrderNumber == CustomerOrderCode);
            //if (!string.IsNullOrEmpty(OrderCode))
            //    predicate = predicate.And(o => o.OrderNumber == OrderCode);
            //if (OrderStatus > 0)
            //    predicate = predicate.And(o => o.OrderState == (Orders.OrderStatus)OrderStatus);
            //if (WarehouseId > 0)
            //    predicate = predicate.And(o => o.Warehouse.WarehouseId == WarehouseId);
            //if (DeliveryType > 0)
            //    predicate = predicate.And(o => o.DeliveryType == (DeliveryTypes)DeliveryType);

            return predicate;
        }
    }
}
