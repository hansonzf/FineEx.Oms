using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqInWarehouseOrderAdd
    {


        /// <summary>
        /// 入库单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 是否退货单
        /// </summary>
        public bool IsGoBackOrder { get; set; }

        /// <summary>
        /// 原发货单号
        /// </summary>
        public string? OriginalOrderCode { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 货主Id(会员Id)
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 入库仓库Id
        /// </summary>
        public int WarehouseID { get; set; }
        /// <summary>
        /// 接口仓Id
        /// </summary>
        public int InterfaceWarehouseID { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string? ErpSyncID { get; set; }
        /// <summary>
        /// ERP单号
        /// </summary>
        public string? ErpSyncCode { get; set; }
        /// <summary>
        /// 入库类型
        /// </summary>
        public int InType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 商品信息List
        /// </summary>
        public List<Goods> GoodList { get; set; }
        /// <summary>
        /// 发货地址Id
        /// </summary>
        public string FromAddressId { get; set; }

        /// <summary>
        /// 发货人单位
        /// </summary>
        public string FromAddressName { get; set; }

        /// <summary>
        /// 发货人名称
        /// </summary>
        public string FromContacter { get; set; }

        /// <summary>
        /// 发货人手机号
        /// </summary>
        public string FromPhone { get; set; }

        /// <summary>
        /// 发货省
        /// </summary>
        public string FromProvince { get; set; }

        /// <summary>
        /// 发货市
        /// </summary>
        public string FromCity { get; set; }
        /// <summary>
        /// 发货区
        /// </summary>
        public string FromArea { get; set; }

        /// <summary>
        /// 发货地址
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 期望送达时间
        /// </summary>
        public DateTime ExpectDeliveryDate { get; set; }
    }

    public class InProduct
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

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

        ///// <summary>
        ///// 良次品
        ///// </summary>
        //public int ProductType { get; set; }
        /// <summary>
        /// 良次品
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
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
