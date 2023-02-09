namespace InventoryCenter.Client
{
    /// <summary>
    /// 入库在途库存审核  自动作业
    /// </summary>
    public class InventoryAuditWarehouseInRequest
    {
        /// <summary>
        /// 入库单类型
        /// </summary>
        public int InType { get; set; }
        /// <summary>
        /// 业务类型 
        /// </summary>
        public int BusinessType { get; set; }

        /// <summary>
        /// 入库单ID
        /// </summary>
        public long BusinessID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 接口仓库ID
        /// </summary>
        public int WarehouseID { get; set; }

        /// <summary>
        /// 单据集合
        /// </summary>
        public List<WarehouseInDetail> InDetailList { get; set; }
    }

    /// <summary>
    /// 请求的单据信息
    /// </summary>
    public class WarehouseInDetail
    {
        /// <summary>
        /// 明细ID
        /// </summary>
        public long InDetailID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 良次废品
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 批次ID
        /// </summary>
        public long ProductBatchID { get; set; }
    }
}
