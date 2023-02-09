using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum TransportTypes
    {
        [Description("发运")]
        Transfer = 1,
        [Description("退货")]
        Return,
        [Description("调拨")]
        Allot
    }
}
