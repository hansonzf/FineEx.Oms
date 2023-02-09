using Oms.Domain.Orders;

namespace Oms.Application.Orders
{
    public class OutboundOrderDto : BusinessOrderDto
    {
        public long OutboundId { get; set; }
        public CustomerDescription Customer { get; set; }
        public CargoOwnerDescription CargoOwner { get; set; }
        public WarehouseDescription Warehouse { get; set; }
        public DeliveryTypes DeliveryType { get; set; }
        public OutboundTypes OutboundType { get; set; }
        public string Remark { get; set; }
        public string CombinationCode { get; set; }
        public int MergedOrderCount { get; set; }
        public DateTime ExpectingOutboundTime { get; set; }
        public DateTime FactOutboundTime { get; set; }
        public AddressDescription DeliveryInfo { get; set; }
        public List<CheckoutProduct> Details { get; set; }

    }
}
