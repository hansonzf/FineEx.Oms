namespace Oms.Domain.Orders
{
    public class MatchTransportLineForOrdersEvent
    {
        public Guid? OrderId { get; set; }
        public IEnumerable<long> OrderIds { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public string TenantId { get; set; }
    }
}
