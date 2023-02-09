using Oms.Domain.Orders;

namespace Oms.Application.Orders
{
    public class TransportOrderDto : BusinessOrderDto
    {
        public long TransportId { get; set; }
        public bool IsReturnBack { get; set; }
        public TransportTypes TransportType { get; set; }
        public CustomerDescription Customer { get; set; }
        public AddressDescription Sender { get; set; }
        public AddressDescription Receiver { get; set; }
        public string Remark { get; set; }
        public ConsignStatus ConsignState { get; set; }
        public List<TransitCargo> Details { get; set; }
        public DateTime ExpectingPickupTime { get; set; }
        public DateTime? FactPickupTime { get; set; }
    }
}
