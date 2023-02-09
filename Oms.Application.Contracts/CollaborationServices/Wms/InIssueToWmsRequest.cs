using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.CollaborationServices.Wms
{

    public class InIssueToWmsRequest
    {
        public List<InIssueToWmsRequestDetail> InOrders { get; set; }
    }

    /// <summary>
    /// 入库单下发 自动作业实体
    /// </summary>
	public class InIssueToWmsRequestDetail
    {
        /// <summary>
        /// 上游交互ID
        /// </summary>
        public long UpSyncID { get; set; }

        /// <summary>
        /// 入库ID
        /// </summary>
        public long InID { get; set; }

        /// <summary>
        /// 下游交互ID
        /// </summary>
        public long DownSyncID { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string InCode { get; set; }

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
        /// ERP同步单号
        /// </summary>
        public string ErpSyncID { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public int InType { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public int OrderSourceID { get; set; }

        /// <summary>
        /// 1贵品0非贵品 默认0
        /// </summary>
        public int IsLuxury { get; set; }


        /// <summary>
        /// 总数
        /// </summary>
        public int TotalAmout { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// ERP单据类型；反馈使用
        /// </summary>
        public string SendType { get; set; }

        /// <summary>
        /// 是否允许低于验收标准收货(0：允许   1：不允许)  默认 0
        /// </summary>
        public int IsValidate { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 报检单号(海关用)
        /// </summary>
        public string DeclNo { get; set; }

        /// <summary>
        /// 载货单号(海关用)
        /// </summary>
        public string ManifestNo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// ERP单号
        /// </summary>
        public string ErpSyncCode { get; set; }

        /// <summary>
        /// 调拨出库仓库ID
        /// </summary>
        public int DbckWareHouseID { get; set; }
        /// <summary>
        /// 调拨出库单号
        /// </summary>
        public string DbckOrderCode { get; set; }

        /// <summary>
        /// 面单号
        /// </summary>
        public string CarryCode { get; set; }

        /// <summary>
        /// 入库明细
        /// </summary>
        public List<InOrderDetailList> DetailList { get; set; }

        /// <summary>
        /// 物流码明细
        /// </summary>
        public List<LogisticsDetail> LogisticsCodeList { get; set; }
        /// <summary>
        /// 个人约定明细
        /// </summary>
        public List<InAppointDetailList> AppointDetailList { get; set; }
        /// <summary>
        /// 入库箱唛信息
        /// </summary>
        public List<BoxCollectionList> BoxCollectionList { get; set; }
    }

    /// <summary>
    /// 入库明细
    /// </summary>
    public class InOrderDetailList
    {
        /// <summary>
        /// 入库提交明细ID
        /// </summary>
        public long InDetailID { get; set; }
        /// <summary>
        /// 入库提交明细ID(下发)
        /// </summary>
        public long DetailID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 提交批次编码
        /// </summary>
        public string ProductBatch { get; set; }

        /// <summary>
        /// 良次品
        /// </summary>
        public int StockType { get; set; }

        /// <summary>
        /// 计划入库数
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 批次ID
        /// </summary>
        public long ProductBatchID { get; set; }

        /// <summary>
        ///行号
        /// </summary>
        public string LineNo { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary> 
        public string ProductionDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string ExpirationDate { get; set; }

        /// <summary>
        /// 货值
        /// </summary>
        public string CurrencyValue { get; set; }

        /// <summary>
        /// 征税方式
        /// </summary>
        public string DutyMode { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Uom { get; set; }

    }
    /// <summary>
    /// 物流码明细
    /// </summary>
    public class LogisticsDetail
    {

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }

        /// <summary>
        /// 物流码
        /// </summary>
        public string LogisticsCode { get; set; }

    }
    /// <summary>
    /// 约定明细
    /// </summary>
    public class InAppointDetailList
    {
        /// <summary>
        /// ERP行号，接口反馈使用
        /// </summary>
        public string LineNo { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductionDate { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 货柜号
        /// </summary>
        public string ContainerNo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        ///  指定批次
        /// </summary>
        public string BatchCode { get; set; }
        /// <summary>
        /// 良次品 1良品2次品 3 废品 默认 1
        /// </summary>
        public int StockType { get; set; }
    }
    /// <summary>
    /// 入库箱唛信息(可空)
    /// </summary>
    public class BoxCollectionList
    {
        public long BoxID { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string BoxCode { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 采集来源 1手工 2接口
        /// </summary>
        public int CollectionSource { get; set; }
        /// <summary>
        /// 采集人
        /// </summary>
        public int CollectionBy { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public string CollectionTime { get; set; }

        /// <summary>
        /// 入库箱唛明细信息
        /// </summary>
        public List<BoxCollectionDetailList> BoxCollectionDetailList { get; set; }
    }
    /// <summary>
    /// 入库箱唛明细信息(可空)
    /// </summary>
    public class BoxCollectionDetailList
    {
        public long BoxID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 良次品 1良品2次品
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 采集数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 批次ID  可以为0
        /// </summary>
        public int ProductBatchID { get; set; }
        /// <summary>
        /// 批次采购单号
        /// </summary>
        public string ProductBatchInCode { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProductionDate { get; set; }
        /// <summary>
        /// 到期日期
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// 指定批次
        /// </summary>
        public string BatchCode { get; set; }
    }
}
