using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Quartz;
using Volo.Abp;

namespace Oms.Application
{
    public static class JobHelper
    {
        public static readonly string TaskBuilderJobTriggerName = "TW(taskbuilder)";
        public static readonly string GlobalTriggerGroupName = "global";

        public static JobKey GetJobKey(Guid orderId, BusinessTypes businessType)
        {
            string groupName = Enum.GetName(businessType);
            if (string.IsNullOrEmpty(groupName))
                throw new BusinessException(message: "The parameter 'businessType' contains error value!");

            return new JobKey(orderId.ToString("N"), groupName);
        }

        public static TriggerKey GetDefaultTriggerKey(Guid orderId, ProcessingSteps step)
        {
            string groupName = Enum.GetName(step);
            if (string.IsNullOrEmpty(groupName))
                throw new BusinessException(message: "The parameter 'step' contains error value!");

            return new TriggerKey(string.Format("TG({0})", orderId.ToString("N")), groupName);
        }
    }
}
