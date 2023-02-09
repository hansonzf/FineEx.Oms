using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum OutboundTypes
    {
        /// <summary>
        /// 退供出库
        /// </summary>
        ReturnSupply = 1,
        /// <summary>
        /// 调拨出库
        /// </summary>
        Allocation = 2,
        /// <summary>
        /// 盘亏出库
        /// </summary>
        CheckLost = 3,
        /// <summary>
        /// 调仓出库
        /// </summary>
        ExchangeWarehouse = 4,
        /// <summary>
        /// 调整出库
        /// </summary>
        AdjustOutbound = 5,
        [Description("B2B出库")]
        B2bOutbound = 6,
        /// <summary>
        /// 普通出库
        /// </summary>
        CommonOutbound = 7,
        /// <summary>
        /// 其他出库
        /// </summary>
        OtherOutbound = 9,
        /// <summary>
        /// 订单转出库
        /// </summary>
        OrderToOutbound = 10,
        /// <summary>
        /// 加工出库
        /// </summary>
        ProcessingOutbound = 11,
        /// <summary>
        /// 转换出库
        /// </summary>
        ConvertOutbound = 12,
        /// <summary>
        /// 领用出库
        /// </summary>
        ConsumingOutbound = 13,
        /// <summary>
        /// 报废出库
        /// </summary>
        ScrapOutbound = 14,
        /// <summary>
        /// B端销售出库
        /// </summary>
        BusinessSalesOutbound = 15,
        /// <summary>
        /// 生产领料出库
        /// </summary>
        ProduceConsumingOutbound = 16,
        /// <summary>
        /// JIT出库
        /// </summary>
        JITOutbound = 17,
        /// <summary>
        /// 临期出库
        /// </summary>
        AdventOutbound = 18,
        /// <summary>
        /// 虚拟出库
        /// </summary>
        VirtualOutbound = 99
    }
}
