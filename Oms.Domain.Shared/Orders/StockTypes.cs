using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum StockTypes
    {
        [Description("良品")]
        Saleable = 0,
        [Description("次品")]
        Damaged = 1
    }
}
