using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    /// <summary>
    /// 订单总枚举
    /// </summary>
    public enum OrderStatusEnum : int
    {
        待处理 = 10,
        待下发 = 20,
        已下发待拣货 = 30,
        已配载 = 40,
        待提货 = 50,
        拣货中 = 60,
        分拣完成待出库 = 70,
        已出库待装车 = 80,
        运输中 = 90,
        转运中 = 100,
        派送中 = 110,
        已签收 = 120,
        已入库 = 130,
        已取消 = 140,
    }
    /// <summary>
    /// 出库订单枚举
    /// </summary>
    public enum OutOrderStatusEnum
    {
        待处理 = 10,
        待下发 = 20,
        已下发待拣货 = 30,
        已配载 = 40,
        拣货中 = 60,
        分拣完成待出库 = 70,
        已出库待装车 = 80,
        运输中 = 90,
        转运中 = 100,
        派送中 = 110,
        已签收 = 120,
        已取消 = 140,
    }
    /// <summary>
    /// 入库订单枚举
    /// </summary>
    public enum InOrderStatusEnum
    {
        待处理 = 10,
        待下发 = 20,
        已下发待拣货 = 30,
        待提货 = 50,
        运输中 = 90,
        转运中 = 100,
        派送中 = 110,
        已签收 = 120,
        已入库 = 130,
        已取消 = 140,
    }
    /// <summary>
    /// 运输订单枚举
    /// </summary>
    public enum TransOrderStatusEnum : int
    {
        待处理 = 10,
        待下发 = 20,
        已下发待拣货 = 30,
        待提货 = 50,
        运输中 = 90,
        转运中 = 100,
        派送中 = 110,
        已签收 = 120,
        已取消 = 140,
    }

    public enum StockAuditStatusEnum : int
    {
        全部审核 = 1,
        缺货 = 2,
        部分审核 = 3,
        未审核 = 4
    }

    /// <summary>
    /// 仓配一体出库：配送类型枚举
    /// </summary>
    public enum DeliveryTypeEnum : int
    {
        物流配送 = 1,
        自提 = 2
    }

    /// <summary>
    /// 仓配一体出库：出库类型枚举
    /// </summary>
    public enum OutWarehouseTypeEnum : int
    {
        退供出库 = 1,
        调拨出库 = 2,
        盘亏出库 = 3,
        调仓出库 = 4,
        调整出库 = 5,
        B2B出库 = 6,
        普通出库 = 7,
        其他出库 = 9,
        订单转出库 = 10,
        加工出库 = 11,
        转换出库 = 12,
        领用出库 = 13,
        报废出库 = 14,
        B端销售出库 = 15,
        生产领料出库 = 16,
        JIT出库 = 17,
        临期出库 = 18,
        虚拟出库 = 99
    }

    /// <summary>
    /// 良次品类别枚举
    /// </summary>
    public enum StockTypeEnum
    {
        良品 = 1,
        次品 = 2,
        废品 = 3
    }
}
