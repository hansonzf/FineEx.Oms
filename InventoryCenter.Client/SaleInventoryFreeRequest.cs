namespace InventoryCenter.Client
{
    public class SaleInventoryFreeRequest
    {
        /// <summary>
        /// 业务类型 1 B2C 2 B2B
        /// </summary>
        public int BusinessType { get; set; }

        /// <summary>
        /// 单据集合
        /// </summary>
        public List<OrderModel> OrderList { get; set; }
    }

    /// <summary>
    /// 请求的单据信息
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public long OutBoundID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 接口仓库ID
        /// </summary>
        public int WarehouseID { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 操作类型1.挂起 2.取消
        /// </summary>
        public int OperationType { get; set; }
    }
}
