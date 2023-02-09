using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqExportOutWarehouseOrder
    {
        /// <summary>
        /// 日期类别（1=创建时间 2=期望出库时间 3=实际出库时间 4=期望送达时间 5=实际签收时间）
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 时间范围
        /// </summary>
        public List<DateTime> TimeRange { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 货主名称
        /// </summary>
        public int ConsignorId { get; set; }
        /// <summary>
        /// 客户外部单号
        /// </summary>
        public string CustomerOrderCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string TMSTransportCode { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 库存审核状态
        /// </summary>
        public int StockAuditStatus { get; set; }
        /// <summary>
        /// 出库仓库
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string WMSOutWarehouseCode { get; set; }
        /// <summary>
        /// 配送类型
        /// </summary>
        public int DeliveryType { get; set; }
    }
}
