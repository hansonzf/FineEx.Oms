using Oms.Domain.Orders;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Oms.Domain.Processings
{
    public class Processing : BasicAggregateRoot<Guid>
    {
        private readonly List<ProcessingSteps> entireSteps = ProcessingStepsConstant.FullyProcessingSteps;
        public Guid OrderId { get; protected set; }
        public BusinessTypes BusinessType { get; protected set; }
        public ProcessingSteps Steps { get; protected set; }
        public int Processed { get; protected set; }
        public long SerialNumber { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? LastTaskBuiltAt { get; protected set; }
        public ProcessingJob? Job { get; protected set; }
        public bool IsScheduled { get; protected set; }
        public int ExecutedCount { get; protected set; }
        public bool IsCompleteAllSteps => (int)Steps == Processed;

        protected Processing()
        {
            Processed = 0;
            CreatedAt = DateTime.Now;
            SerialNumber = CreatedAt.Ticks;
        }

        public Processing(Guid orderId, BusinessTypes businessType)
            : this()
        {
            OrderId = orderId;
            IsScheduled = false;
            BusinessType = businessType;
            Steps = InitialSteps(businessType);
        }

        static ProcessingSteps InitialSteps(BusinessTypes businessType)
        {
            ProcessingSteps steps = businessType switch
            {
                BusinessTypes.OutboundWithTransport => ProcessingStepsConstant.OutboundWithTransportSteps,
                BusinessTypes.InboundWithTransport => ProcessingStepsConstant.InboundWithTransportSteps,
                BusinessTypes.Transport => ProcessingStepsConstant.TransportSteps,
                _ => throw new ArgumentException(nameof(businessType))
            };

            return steps;
        }

        public ProcessingSteps GetCurrentStep()
        {
            ProcessingSteps remainSteps = (ProcessingSteps)((int)Steps - Processed);
            foreach (var item in entireSteps)
            {
                if ((remainSteps & item) != 0)
                    return item;
            }

            return ProcessingSteps.None;
        }


        public void BuildingTask(double delayMilliseconds = 0)
        {
            var proc = GetCurrentStep();
            IsScheduled = false;
            LastTaskBuiltAt = DateTime.Now;
            AddLocalEvent(new BuildingTaskEvent
            {
                CurrentStep = proc,
                BusinessType = BusinessType,
                OrderId = OrderId,
                ProcessingId = Id,
                DelayMillisecondsStart = delayMilliseconds
            });
        }

        public void SetBuiltTaskResult(string jobName, string groupName, string triggerName, string triggerGroup)
        {
            Job = new ProcessingJob(jobName, groupName, triggerName, triggerGroup);
            IsScheduled = true;
        }

        public void CompleteTask(ProcessingSteps step, bool taskResponsed = true)
        {
            var currentStep = GetCurrentStep();
            if (step != currentStep)
                throw new ArgumentException(nameof(step));

            if (step == ProcessingSteps.B2bCheckoutInventory && !taskResponsed)
                return;
            
            Processed |= (int)step;
            Job = ProcessingJob.Empty;
            IsScheduled = false;
            ExecutedCount = 0;
        }

        public void ExecuteFailed(ProcessingSteps step)
        {
            var currentStep = GetCurrentStep();
            if (step != currentStep)
                throw new ArgumentException(nameof(step));

            Job = ProcessingJob.Empty;
            IsScheduled = false;
            ExecutedCount++;
        }

        public void CancelJob()
        {
            Job = ProcessingJob.Empty;
            Processed = (int)Steps;
            IsScheduled = false;
        }

        public void StartRun(ProcessingSteps proc)
        {
            var currentProc = GetCurrentStep();
            if (currentProc != proc)
                throw new BusinessException(message: $"The order can not execute {Enum.GetName(currentProc)} task");

            if (!IsScheduled)
                BuildingTask();

            AddDistributedEvent(new StartRunTaskEvent { 
                ProcessingId = Id,
                CurrentTask = proc
            });
        }
    }
}
