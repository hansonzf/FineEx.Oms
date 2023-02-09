using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum ConsignStatus
    {
        None,
        [Description("待提货")]
        WaitforPickup,
        [Description("运输中")]
        Transferring,
        [Description("待派送")]
        ReadyToDispatch,
        [Description("派送中")]
        Dispatching,
        [Description("已签收")]
        Receipt,
        [Description("已取消")]
        Cancelled
    }
}
