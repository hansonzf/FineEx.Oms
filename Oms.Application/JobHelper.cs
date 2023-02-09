using Oms.Domain.Processings;
using Quartz;

namespace Oms.Application
{
    public static class JobHelper
    {
        public static readonly string PublicTriggerGroupName = "public";
        public static readonly string GlobalTriggerGroupName = "global";

        public static JobKey GetJobKey(Guid orderId, ProcessingSteps step)
        {
            return new JobKey(
                orderId.ToString("N"), 
                Enum.GetName(step));
        }

        public static TriggerKey GetPublicTriggerKey()
        {
            return new TriggerKey(
                DateTime.Now.ToString("yyyyMMdd"), 
                PublicTriggerGroupName);
        }

        public static TriggerKey GetPrivateTriggerKey(Guid orderId, ProcessingSteps step)
        {
            return new TriggerKey(
                string.Format("TG({0})", orderId.ToString("N")), 
                string.Format("private({0})", Enum.GetName(step)));
        }
    }
}
