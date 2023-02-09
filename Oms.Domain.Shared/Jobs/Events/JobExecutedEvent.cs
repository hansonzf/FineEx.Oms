using Oms.Domain.Orders;
using Oms.Domain.Processings;

namespace Oms.Domain.Jobs
{
    public class JobExecutedEvent
    {
        public Guid OrderId { get; set; }
        public ProcessingSteps Proc { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public bool IsSuccess { get; set; }
    }
}
