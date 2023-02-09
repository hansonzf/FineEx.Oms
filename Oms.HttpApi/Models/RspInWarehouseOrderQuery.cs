using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspInWarehouseOrderQuery
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 明细数据
        /// </summary>
        public List<InWarehouseOrderQueryModel> Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        public class InWarehouseOrderQueryModel
        {

            /// <summary>
            /// 入库单Id
            /// </summary>
            public long InID { get; set; }

            /// <summary>
            /// 订单号
            /// </summary>
            public string InCode { get; set; }

            /// <summary>
            /// 上游交互ID
            /// </summary>
            public string UpSyncID { get; set; }

            /// <summary>
            /// 客户外部单号
            /// </summary>
            public string CustomerOrderCode { get; set; }

            /// <summary>
            /// 原发货单号
            /// </summary>
            public string SourceOrderCode { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public string CreateDate { get; set; }

            /// <summary>
            /// 订单状态
            /// </summary>
            public int OrderStatusNew { get; set; }

            /// <summary>
            /// 订单状态名称
            /// </summary>
            public string OrderStatusName { get; set; }

            /// <summary>
            /// 运输方案Id
            /// </summary>
            public long OrderTranPlanId { get; set; }

            /// <summary>
            /// 运输方案名称
            /// </summary>
            public string OrderTranPlanName { get; set; }

            /// <summary>
            /// 客户名称
            /// </summary>
            public string CustomerName { get; set; }

            /// <summary>
            /// 货主名称
            /// </summary>
            public string CargoOwnerName { get; set; }

            /// <summary>
            /// 入库仓库名称
            /// </summary>
            public string WarehouseName { get; set; }

            /// <summary>
            /// 入库单号
            /// </summary>
            public string InWarehouseCode { get; set; }

            /// <summary>
            /// 入库类型
            /// </summary>
            public int InType { get; set; }

            /// <summary>
            /// 入库类型名称
            /// </summary>
            public string InTypeName { get; set; }

            /// <summary>
            /// 提交件数
            /// </summary>
            public int SumbitNumber { get; set; }

            /// <summary>
            /// 确认件数
            /// </summary>
            public int ComfirmNumber { get; set; }

            /// <summary>
            /// 运单号
            /// </summary>
            public string TransOrderCode { get; set; }

            /// <summary>
            /// 发货人单位
            /// </summary>
            public string SenderUnit { get; set; }

            /// <summary>
            /// 发货人
            /// </summary>
            public string Sender { get; set; }

            /// <summary>
            /// 发货人手机号
            /// </summary>
            public string SenderPhone { get; set; }

            /// <summary>
            /// 发货地址
            /// </summary>
            public string SenderAddress { get; set; }

            /// <summary>
            /// 期望送达时间
            /// </summary>
            public string ExpectDeliveryDate { get; set; }

            /// <summary>
            /// 入库时间
            /// </summary>
            public string InWarehouseDateTime { get; set; }

            /// <summary>
            /// 订单来源
            /// </summary>
            public int OrderSourceId { get; set; }
            /// <summary>
            /// 订单来源
            /// </summary>
            public string OrderSource { get; set; }
        }
    }
}
