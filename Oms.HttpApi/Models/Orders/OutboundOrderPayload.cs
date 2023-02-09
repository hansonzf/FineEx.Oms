using Oms.Domain.Orders;
using System.ComponentModel.DataAnnotations;

namespace Oms.HttpApi.Models.Orders
{
    public class OutboundOrderPayload
    {
        [Required]
        [StringLength(16)]
        public string OrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public CargoOwnerModel CargoOwner { get; set; }
        public string Remark { get; set; }
        [Range(1, int.MaxValue)]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int DeliveryType { get; set; }
        [Required]
        public DeliveryModel DeliveryInfo { get; set; }
        public List<CheckoutProductDetailModel> CheckoutProducts { get; set; }
    }

    public class CheckoutProductDetailModel
    {
        public int ProductId { get; set; }
        public string ProductBatch { get; set; }
        public string SKU { get; set; }
        public StockTypes StockType { get; set; }
        public int RequiredQty { get; set; }
    }
}
