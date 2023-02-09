using System.Text.Json.Serialization;

namespace InventoryCenter.Client
{
    public class SalesInventoryAuditRequest
    {
        /// <summary>
        /// 审核通过类型 1 缺货不过 2 缺货也过
        /// </summary>
        public int ModelType { get; set; }

        /// <summary>
        /// 业务类型 1 B2C 2 B2B
        /// </summary>
        public int BusinessType { get; set; }

        /// <summary>
        /// 单据集合
        /// </summary>
        public List<OrderInfoModel> OrderList { get; set; }
    }

    /// <summary>
    /// 请求的单据信息
    /// </summary>
    public class OrderInfoModel
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public long OutBoundID { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        [JsonIgnoreAttribute]
        public string OrderCode { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 接口仓库ID
        /// </summary>
        public int WarehouseID { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 单据明细集合
        /// </summary>
        public List<OrderInfoDetailModel> DetailList { get; set; }
    }

    /// <summary>
    /// 单据明细信息
    /// </summary>
    public class OrderInfoDetailModel
    {
        /// <summary>
        /// 明细ID
        /// </summary>
        public long OrderDetailID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 库存类型 1 良品 2 次品
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 批次信息
        /// </summary>
        public string ProductBatch { get; set; }
    }
}
