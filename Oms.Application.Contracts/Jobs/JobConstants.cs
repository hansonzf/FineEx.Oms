namespace Oms.Application.Jobs
{
    public static class JobConstants
    {
        public static readonly string JobDataMapOrderIdKeyName = "orderId";
        public static readonly string JobDataMapBusinessTypeKeyName = "businessType";





        public static readonly string PrivateTriggerGroup = "default-triggers";
        public static readonly string PublicTriggerGroup = "public-triggers";

        public static readonly string JobDataMapRequestKeyName = "api-request-parameter";
        public static readonly string JobDataMapPartitionKeyName = "api-request-partition-parameter";
        public static readonly string JobDataMapDispatchToWmsKeyName = "dispatch-to-wms";
        public static readonly string JobDataMapDispatchToTmsKeyName = "dispatch-to-tms";
    }
}
