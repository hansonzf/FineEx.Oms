using System.ComponentModel.DataAnnotations;

namespace Oms.HttpApi.Models.Orders
{
    public class TransportOrderPayload
    {
        public int TransportType { get; set; }
        public string OrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public CargoOwnerModel CargoOwner { get; set; }
        public string Remark { get; set; }
        [Required]
        public DeliveryModel Sender { get; set; }
        [Required]
        public DeliveryModel Receiver { get; set; }
        public List<CargoModel> CargoDetails { get; set; }
    }

    public class CargoModel
    {
        public string CargoName { get; set; }
        public int PackageCount { get; set; }
        public int InnerPackageCount { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
    }
}
