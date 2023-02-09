namespace Oms.Application.Jobs
{
    public record ProcessingJobDto
    {
        public string JobName { get; init; }
        public string GroupName { get; init; }
        public string TriggerName { get; init; }
        public string TriggerGroup { get; init; }
    }
}
