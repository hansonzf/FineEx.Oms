using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum BusinessTypes
    {
        None,
        [Description("仓配一体出库业务")]
        OutboundWithTransport = 1,
        [Description("仓配一体入库业务")]
        InboundWithTransport,
        [Description("运输业务")]
        Transport
    }
}
