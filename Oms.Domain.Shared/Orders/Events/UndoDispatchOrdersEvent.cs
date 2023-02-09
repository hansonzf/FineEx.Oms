using Oms.Domain.Orders;

namespace Oms.Domain.Shared.Orders.Events
{
    public class UndoDispatchOrdersEvent
    {
        public long[] OrderIds { get; set; }
        public string TenantId { get; set; }
        public BusinessTypes BusinessType { get; set; }
    }
}
