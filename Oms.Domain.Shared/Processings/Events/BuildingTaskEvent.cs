using Oms.Domain.Orders;

namespace Oms.Domain.Processings
{
    public class BuildingTaskEvent
    {
        public ProcessingSteps CurrentStep { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public Guid OrderId { get; set; }
        public string TenantId { get; set; }
        public Guid ProcessingId { get; set; }
        public double DelayMillisecondsStart { get; set; }
    }
}
