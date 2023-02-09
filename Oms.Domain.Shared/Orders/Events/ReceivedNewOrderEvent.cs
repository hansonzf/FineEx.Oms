namespace Oms.Domain.Orders
{
    public class ReceivedNewOrderEvent
    {
        public Guid OrderId { get; set; }
        public BusinessTypes BusinessType { get; set; }
    }
}
