namespace Oms.Application.Contracts.CollaborationServices.Wms
{
    public class OutIssueToWmsRequest
    {
        public List<OutIssueToWmsRequestDetail> OutOrders { get; set; }
    }
    /// <summary>
    /// 出库单下发，请求实体
    /// </summary>
    public class OutIssueToWmsRequestDetail
    {
        /// <summary>
        /// 系统交互ID
        /// </summary>
        public long UpSyncID { get; set; }

        public long OutID { get; set; }

        /// <summary>
        /// 系统交互ID
        /// </summary>
        public long DownSyncID { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string OutCode { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 接口仓库ID
        /// </summary>
        public int InterfaceWarehouseID { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int WarehouseID { get; set; }

        /// <summary>
        /// 订单来源（1-100 各种接口 102 手工单据）
        /// </summary>
        public int OrderSourceID { get; set; }

        /// <summary>
        /// 出库类型(1.退供出库2.调拨出库3.盘亏出库4.调仓出库5.调整出库6.B2B出库7.普通出库9.其他出库10.订单转出库11.加工出库12.转换出库13.领用出库14.报废出库15.B端销售出库16.生产领料出库99.虚拟出库)
        /// </summary>
        public int OutType { get; set; }

        /// <summary>
        /// ERP单号 接口单据必传
        /// </summary>
        public string ErpSyncID { get; set; }

        /// <summary>
        /// erp单号
        /// </summary>
        public string ErpSyncCode { get; set; }

        /// <summary>
        /// 快递公司，默认为空
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 承运商
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// 面单号
        /// </summary>
        public string CarryCode { get; set; }

        /// <summary>
        /// 运输方式 1无  2物流 3快递 默认1
        /// </summary>
        public string WayType { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 收件地址 默认为空
        /// </summary>
        public string ConsigneeAddress { get; set; }

        /// <summary> 
        /// 收件电话 默认为空
        /// </summary>
        public string ConsigneePhone { get; set; }

        /// <summary>
        /// 收件手机 默认为空
        /// </summary>
        public string ConsigneeMobilePhone { get; set; }

        /// <summary>
        /// 收件省 默认为空
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 收件市 默认为空
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 收件区  默认为空
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 备注  默认为空
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }

        /// <summary>
        /// ERP单据类型；反馈使用,，默认为空
        /// </summary>
        public string SendType { get; set; }

        /// <summary>
        /// 网易:采购单号 默认为空
        /// </summary>
        public string CGCode { get; set; }

        /// <summary>
        /// 网易:供应商名称 默认为空
        /// </summary>
        public string GYSName { get; set; }

        /// <summary>
        /// 网易:目的城市 默认为空
        /// </summary>
        public string DestCity { get; set; }

        /// <summary>
        /// 网易:目的库房
        /// </summary>
        public string DsetWarehouse { get; set; }

        /// <summary>
        /// 网易:是否需要定制物流箱
        /// </summary>
        public int CustomBox { get; set; }

        /// <summary>
        /// 网易:是否需要DM单
        /// </summary>
        public int DM { get; set; }

        /// <summary>
        /// 网易:是否需要贺卡
        /// </summary>
        public int GreetingCard { get; set; }

        /// <summary>
        /// 网易:指定箱唛规格
        /// </summary>
        public string ShippingMarkSpec { get; set; }

        /// <summary>
        /// 网易:是否指定签收单
        /// </summary>
        public int ReceiveBill { get; set; }

        /// <summary>
        /// 网易:是否装箱清单
        /// </summary>
        public int PackingList { get; set; }

        /// <summary>
        /// 网易:是否商品贴Logo
        /// </summary>
        public int Logo { get; set; }

        /// <summary>
        /// 网易:是否指定日期
        /// </summary>
        public int ExpirationDate { get; set; }

        /// <summary>
        /// 网易:是否整箱发货
        /// </summary>
        public int FCL { get; set; }

        /// <summary>
        /// 网易:面单唯一标识
        /// </summary>
        public string MailNoCode { get; set; }

        /// <summary>
        /// 网易:0:不使用网易面单 1：使用网易面单
        /// </summary>
        public string PrintMailNo { get; set; }

        /// <summary>
        /// 到达地
        /// </summary>
        public string Dest { get; set; }

        /// <summary>
        /// 出库单渠道类型
        /// </summary>
        public string OrderSourceCode { get; set; }

        /// <summary>
        /// 期望出库时间,默认为空
        /// </summary>
        public string DeliveryPlanTime { get; set; }

        /// <summary>
        /// 发件人电话
        /// </summary>
        public string SenderMobile { get; set; }

        /// <summary>
        /// 经营地址
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 是否拼箱 0 否 1 是 默认0
        /// </summary>
        public int SpellBox { get; set; }

        /// <summary>
        /// 销售日期
        /// </summary>
        public string SaleDate { get; set; }

        /// <summary>
        /// 销售公司
        /// </summary>
        public string SaleCompany { get; set; }

        /// <summary>
        /// 目的地仓库(网易效期用)
        /// </summary>
        public string DestWarehouseCode { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// 是否允许少出   1 允许少出  2 不允许少出  默认0
        /// </summary>
        public int LessOut { get; set; }

        /// <summary>
        /// 网点id
        /// </summary>
        public int NetPointID { get; set; }

        /// <summary>
        /// 其他出库类型
        /// </summary>
        public string OtherOutType { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
		/// Customize
		/// </summary>
		public int Customize { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 审核规则 0 正常 1 按一审出库
        /// </summary>
        public int AuditRule { get; set; }
        /// <summary>
		/// 发件人
		/// </summary>
		public string Sender { get; set; }
        /// <summary>
        /// 发件省
        /// </summary>
        public string SenderProvince { get; set; }
        /// <summary>
        /// 发件市
        /// </summary>
        public string SenderCity { get; set; }
        /// <summary>
        /// 发件区
        /// </summary>
        public string SenderArea { get; set; }
        /// <summary>
        /// 发件地址
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// 社区ID
        /// </summary>
        public int CommunityID { get; set; }

        /// <summary>
        /// 出库单提交明细集合
        /// </summary>
        public List<OutDetailRequest> DetailList { get; set; }

        /// <summary>
        /// 出库单审核明细集合
        /// </summary>
        public List<OutAuditDetailRequest> AuditDetailList { get; set; }
        /// <summary>
        /// 伊丽特明细集合
        /// </summary>
        public List<OutYltDetailList> YltDetailList { get; set; }
    }

    /// <summary>
    /// 出库单提交明细
    /// </summary>
    public class OutDetailRequest
    {
        /// <summary>
        /// 出库明细ID
        /// </summary>
        public long OutDetailID { get; set; }
        /// <summary>
        /// 出库单明细ID（下发）
        /// </summary>
        public long DetailID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 良次品(1 良 2次 3 废)
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 批次编码
        /// </summary>
        public string ProductBatch { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// ERP行号，接口反馈使用  默认为空
        /// </summary>
        public string LineNo { get; set; }

        /// <summary>
        /// 单价  默认 0.00
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 总价  默认 0.00
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// 拣货规则  默认 0
        /// </summary>
        public string PickRules { get; set; }
        /// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
        /// <summary>
		/// 原厂供应商物料号
		/// </summary>
		public string SupplierSku { get; set; }
        /// <summary>
        /// ODM物料料号
        /// </summary>
        public string CustomerSku { get; set; }
        /// <summary>
        /// ODM PO
        /// </summary>
        public string CustomerPo { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }

    /// <summary>
    /// 出库明细
    /// </summary>
    public class OutDetailRequestTemp
    {
        /// <summary>
        /// 出库单ID 
        /// </summary>
        public long OutID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 批次编码
        /// </summary>
        public string ProductBatch { get; set; }

        /// <summary>
        /// 批次ID
        /// </summary>
        public long ProductBatchID { get; set; }

        /// <summary>
        /// 良次品(1 良 2次 3 废)
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// ERP行号，接口反馈使用  默认为空
        /// </summary>
        public string LineNo { get; set; }

        /// <summary>
        /// 单价  默认 0.00
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总价  默认 0.00
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// 拣货规则  默认 0
        /// </summary>
        public decimal PickRules { get; set; }

        /// <summary>
        /// 是否指定批次
        /// </summary>
        public int IsSpecifiedBatch { get; set; }
    }

    /// <summary>
    /// 出库单审核明细
    /// </summary>
    public class OutAuditDetailRequest
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 批次ID
        /// </summary>
        public long ProductBatchID { get; set; }

        /// <summary>
        /// 良次品(1 良 2次 3 废)
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 审核数量
        /// </summary>
        public int PickAmount { get; set; }

        /// <summary>
        /// 是否指定批次 0 否 1 是
        /// </summary>
        public int IsSpecifiedBatch { get; set; }
    }

    /// <summary>
    /// 伊丽特明细集合
    /// </summary>
    public class OutYltDetailList
    {

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 库存状态 1 正品 2 次品
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 类型2=学校→班级→个人,3=学校→班级,4=学校→个人
        /// </summary>
        public int YltType { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StuName { get; set; }
        /// <summary>
        /// 性别，0男，1女 默认=0
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 款号 打印用
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 尺码 打印用
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 商品名称 打印用
        /// </summary>
        public string ItemName { get; set; }

    }
}
