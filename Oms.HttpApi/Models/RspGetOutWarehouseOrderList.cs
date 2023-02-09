using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspGetOutWarehouseOrderList
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SeqNo { get; set; }
        /// <summary>
        /// 出库订单id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public int MemberID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 已合并订单数量
        /// </summary>
        public int MergeOrderQty { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string CustomerOrderCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 库存审核状态
        /// </summary>
        public string StockAuditStatusName { get; set; }
        /// <summary>
        /// 运输方案
        /// </summary>
        public string TranPlanName { get; set; }
        /// <summary>
        /// 运输方案id
        /// </summary>
        public long OrderTranPlanID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 货主名称
        /// </summary>
        public string ConsignorName { get; set; }
        /// <summary>
        /// 出货仓库
        /// </summary>
        public string WarehouseName { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string WMSOutWarehouseCode { get; set; }
        /// <summary>
        /// 出库类型
        /// </summary>
        public string OutWarehouseTypeName { get; set; }
        /// <summary>
        /// 期望出库时间
        /// </summary>
        public string PlanOutWarehouseDate { get; set; }
        /// <summary>
        /// 实际出库时间
        /// </summary>
        public string ActualOutWarehouseDate { get; set; }
        /// <summary>
        /// 提交件数
        /// </summary>
        public int SubmitQty { get; set; }
        /// <summary>
        /// 确认件数
        /// </summary>
        public int ConfirmQty { get; set; }
        /// <summary>
        /// 配送类型
        /// </summary>
        public string DeliveryTypeName { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string TMSTransportCode { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ConsigneeUnit { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeName { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 期望送达时间
        /// </summary>
        public string ExpectDeliveryDate { get; set; }
        /// <summary>
        /// 实际签收时间
        /// </summary>
        public string ActSignDate { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSourceIdName { get; set; }


    }
}
