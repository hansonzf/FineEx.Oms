using System.ComponentModel.DataAnnotations;

namespace Oms.HttpApi.Models.Orders
{
    public class InboundOrderPayload
    {
        [Required]
        [StringLength(16)]
        public string OrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public bool IsReturnOrder { get; set; }
        public string OriginDeliveryOrderNumber { get; set; }
        public int DeliveryType { get; set; }
        public CargoOwnerModel CargoOwner { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int InboundType { get; set; }
        public string? Remark { get; set; }
        public DeliveryModel DeliveryInfo { get; set; }
        public List<CheckinProductModel> CheckinProducts { get; set; }
    }

    public class CheckinProductModel
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public int Qty { get; set; }
    }
}