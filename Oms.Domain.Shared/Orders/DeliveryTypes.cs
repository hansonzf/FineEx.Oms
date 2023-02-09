using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum DeliveryTypes
    {
        [Description("物流配送")]
        Delivery = 1,
        [Description("客户自提")]
        FetchBySelf
    }
}
