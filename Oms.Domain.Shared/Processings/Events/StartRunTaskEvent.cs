using Oms.Domain.Processings;

namespace Oms.Domain.Processings
{
    public class StartRunTaskEvent
    {
        public Guid ProcessingId { get; set; }
        public ProcessingSteps CurrentTask { get; set; }
    }
}
