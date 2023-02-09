using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspOutOrderEditInfo
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 出库修改详情
        /// </summary>
        public OutOrderEditInfoModel Data { get; set; }
        public class OutOrderEditInfoModel
        {
            /// <summary>
            /// 订单id
            /// </summary>
            public long OrderId { get; set; }
            /// <summary>
            /// 订单code
            /// </summary>
            public string OrderCode { get; set; }
            /// <summary>
            /// 货主id
            /// </summary>
            public string CargoowerId { get; set; }
            /// <summary>
            /// 客户id
            /// </summary>
            public string CustomerId { get; set; }
            /// <summary>
            /// 客户单号
            /// </summary>
            public string CustomerOrderCode { get; set; }
            /// <summary>
            /// 出库仓库id
            /// </summary>
            public string WarehouseId { get; set; }
            /// <summary>
            /// 出库类型
            /// </summary>
            public int OutWarehouseType { get; set; }
            /// <summary>
            /// 期望出库时间
            /// </summary>
            public string PlanOutWarehouseDate { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 商品信息
            /// </summary>
            public List<Commodity> CommodityInfoList { get; set; }
            /// <summary>
            /// 配送类型
            /// </summary>
            public int DeliveryType { get; set; }
            /// <summary>
            /// 配送信息
            /// 收货地址id，包含接口收货人（单位、姓名、手机号、详细地址、省市区）
            /// </summary>
            public string ConsigneeId { get; set; }
            /// <summary>
            /// 期望送达时间
            /// </summary>
            public string ExpectDeliveryDate { get; set; }

            public class Commodity
            {
                /// <summary>
                /// 商品id
                /// </summary>
                public string CommodityID { get; set; }
                /// <summary>
                /// 商品条码
                /// </summary>
                public string Barcode { get; set; }
                /// <summary>
                /// 商品编码
                /// </summary>
                public string CommodityCode { get; set; }
                /// <summary>
                /// 商品名称
                /// </summary>
                public string CommodityName { get; set; }
                /// <summary>
                /// 批次编码
                /// </summary>
                public string BatchCode { get; set; }
                /// <summary>
                /// 良次品类别(1=良次品 2=次 3=废)
                /// </summary>
                public string StockType { get; set; }
                /// <summary>
                /// 数量
                /// </summary>
                public int ConfirmQty { get; set; }
                /// <summary>
                /// 体积
                /// </summary>
                public decimal Volumn { get; set; }
                /// <summary>
                /// 重量
                /// </summary>
                public decimal Weight { get; set; }
            }
        }

    }
}
