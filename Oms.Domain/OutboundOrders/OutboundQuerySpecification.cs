using Oms.Domain.Orders;
using System.Linq.Expressions;

namespace Oms.Domain.OutboundOrders
{
    public class OutboundQuerySpecification : Volo.Abp.Specifications.Specification<OutboundOrder>
    {
        public int TimeType { get; set; }
        public List<DateTime> TimeRange { get; set; } = new List<DateTime>();
        public int CustomerId { get; set; }
        public int CargoOwnerId { get; set; }
        public string CustomerOrderCode { get; set; }
        public string OrderCode { get; set; }
        public string TMSTransportCode { get; set; }
        public int OrderStatus { get; set; }
        public int StockAuditStatus { get; set; }
        public int WarehouseId { get; set; }
        public string WMSOutWarehouseCode { get; set; }
        public int DeliveryType { get; set; }

        public override Expression<Func<OutboundOrder, bool>> ToExpression()
        {
            Expression<Func<OutboundOrder, bool>> predicate = o => true;

            if (TimeRange.Count == 2)
            {
                predicate = TimeType switch
                {
                    1 => predicate.And(o => o.ReceivedAt > TimeRange.First() && o.ReceivedAt <= TimeRange.Last()),
                    2 => predicate.And(o => o.ExpectingOutboundTime > TimeRange.First() && o.ExpectingOutboundTime <= TimeRange.Last()),
                    3 => predicate.And(o => o.FactOutboundTime > TimeRange.First() && o.FactOutboundTime <= TimeRange.Last()),
                    _ => predicate
                };
            }

            if (CustomerId > 0)
                predicate = predicate.And(o => o.Customer.CustomerId == CustomerId);
            if (CargoOwnerId > 0)
                predicate = predicate.And(o => o.CargoOwner.CargoOwnerId == CargoOwnerId);
            if (!string.IsNullOrEmpty(CustomerOrderCode))
                predicate = predicate.And(o => o.OrderNumber == CustomerOrderCode);
            if (!string.IsNullOrEmpty(OrderCode))
                predicate = predicate.And(o => o.OrderNumber == OrderCode);
            if (OrderStatus > 0)
                predicate = predicate.And(o => o.OrderState == (Orders.OrderStatus)OrderStatus);
            if (WarehouseId > 0)
                predicate = predicate.And(o => o.Warehouse.WarehouseId == WarehouseId);
            if (DeliveryType > 0)
                predicate = predicate.And(o => o.DeliveryType == (DeliveryTypes)DeliveryType);

            return predicate;
        }
    }
}
