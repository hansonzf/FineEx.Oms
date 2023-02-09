namespace Oms.Domain.Orders
{
    public class DispatchOrdersEvent
    {
        public Guid? OrderId { get; set; }
        public long[] OrderIds { get; set; }
        public string TenantId { get; set; }
        public BusinessTypes BusinessType { get;set; }
    }
}
