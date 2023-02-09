using Volo.Abp.Domain.Values;

namespace Oms.Domain.Processings
{
    public class ProcessingJob : ValueObject
    {
        public string JobName { get; private set; }
        public string GroupName { get; private set; }
        public string TriggerName { get; private set; }
        public string TriggerGroup { get; private set; }

        private ProcessingJob()
        { }

        public ProcessingJob(string jobName, string groupName, string triggerName, string triggerGroup)
        {
            JobName = jobName;
            GroupName = groupName;
            TriggerName = triggerName;
            TriggerGroup = triggerGroup;
        }

        public static ProcessingJob Empty => new ProcessingJob();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return JobName;
            yield return GroupName;
            yield return TriggerName;
            yield return TriggerGroup;
        }
    }
}
