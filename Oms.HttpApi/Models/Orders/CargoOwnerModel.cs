namespace Oms.HttpApi.Models.Orders
{
    public class CargoOwnerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CargoOwnerId { get; set; }
        public string CargoOwnerName { get; set; }
    }
}
