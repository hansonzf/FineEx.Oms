using Oms.Domain.Orders;

namespace Oms.Application.Orders
{
    public class InboundOrderDto : BusinessOrderDto
    {
        public CustomerDescription Customer { get; set; }
        public CargoOwnerDescription CargoOwner { get; set; }
        public WarehouseDescription Warehouse { get; set; }
        public long InboundId { get; set; }
        public bool IsReturnOrder { get; set; }
        public string OriginDeliveryNumber { get; set; }
        public InboundTypes InboundType { get; set; }
        public DeliveryTypes DeliveryType { get; set; }
        public string Remark { get; set; }
        public AddressDescription DeliveryInfo { get; set; }
        public List<CheckinProduct> Details { get; set; }
    }
}
