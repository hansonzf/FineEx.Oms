namespace Oms.HttpApi.Models.Orders
{
    public class DeliveryModel
    {
        public string CompanyName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public DateTime ExpectingTime { get; set; }
    }

}
