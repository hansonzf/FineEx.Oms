using Oms.Domain.Orders;

namespace Oms.Domain.Processings
{
    public class BuildingTaskEvent
    {
        public ProcessingSteps CurrentStep { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProcessingId { get; set; }
        public double DelayStart { get; set; }
    }
}
