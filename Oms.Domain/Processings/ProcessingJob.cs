using Volo.Abp.Domain.Values;

namespace Oms.Domain.Processings
{
    public class ProcessingJob : ValueObject
    {
        public string JobName { get; private set; }
        public string GroupName { get; private set; }
        public string TriggerName { get; private set; }

        private ProcessingJob()
        { }

        public ProcessingJob(string jobName, string groupName, string triggerName)
        {
            JobName = jobName;
            GroupName = groupName;
            TriggerName = triggerName;
        }

        public static ProcessingJob Empty => new ProcessingJob();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return JobName;
            yield return TriggerName;
            yield return GroupName;
        }
    }
}
