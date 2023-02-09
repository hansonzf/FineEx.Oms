namespace Oms.Domain.Orders
{
    public class CheckInventoryforOrderEvent
    {
        public Guid? OrderId { get; set; }
        public IEnumerable<long> OrderIds { get; set; }
        public BusinessTypes BusinessType { get; set; }
    }
}
