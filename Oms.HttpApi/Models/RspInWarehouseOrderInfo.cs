using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspInWarehouseOrderInfo
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 明细数据
        /// </summary>
        public InWarehouseOrderInfoModel Data { get; set; }
    }

    public class InWarehouseOrderInfoModel
    {
        public bool IsGoBackOrder { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long CargoOwnerId { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ErpSyncID { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string InCode { get; set; }

        /// <summary>
        /// 订单状态名称
        /// </summary>
        public int InStatus { get; set; }

        /// <summary>
        /// 订单状态名称
        /// </summary>
        public string OrderStatusName { get; set; }

        /// <summary>
        /// 原发货单号
        /// </summary>
        public string SourceOrderCode { get; set; }
        /// <summary>
        /// 入库通知单号
        /// </summary>
        public string WMSInWarehouseCode { get; set; }

        /// <summary>
        /// 入库仓库Id
        /// </summary>
        public long WarehouseID { get; set; }

        /// <summary>
        /// 入库仓库名称
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public int InType { get; set; }

        /// <summary>
        /// 入库类型名称
        /// </summary>
        public string InTypeName { get; set; }

        /// <summary>
        /// 期望送达时间
        /// </summary>
        public string ExpectDeliveryDate { get; set; }

        /// <summary>
        /// 入库备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 起点地址id
        /// </summary>
        public string FromAddressId { get; set; }

        /// <summary>
        /// 起点地址Name
        /// </summary>
        public string FromAddressName { get; set; }

        /// <summary>
        /// 终点地址id
        /// </summary>
        public string ToAddressId { get; set; }

        /// <summary>
        /// 终点地址Name
        /// </summary>
        public string ToAddressName { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 发件人电话
        /// </summary>
        public string SenderPhone { get; set; }

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
        /// 收件人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary> 
        /// 收件电话 默认为空
        /// </summary>
        public string ConsigneePhone { get; set; }

        /// <summary>
        /// 收件省 默认为空
        /// </summary>
        public string ConsigneeProvince { get; set; }

        /// <summary>
        /// 收件市 默认为空
        /// </summary>
        public string ConsigneeCity { get; set; }

        /// <summary>
        /// 收件区  默认为空
        /// </summary>
        public string ConsigneeArea { get; set; }

        /// <summary>
        /// 收件地址 默认为空
        /// </summary>
        public string ConsigneeAddress { get; set; }

        /// <summary>
        /// 商品信息List
        /// </summary>
        public List<InGoods> GoodList { get; set; }

        /// <summary>
        /// 运单信息list
        /// </summary>
        public List<TransportInfo> TransportInfos { get; set; }

        /// <summary>
        /// 操作日志
        /// </summary>
        public List<OpreationLog> OpreationLogs { get; set; }
    }
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OpreationLog
    {
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 跟踪类型名称
        /// </summary>
        public string TrackTypeName { get; set; }
        /// <summary>
        /// 跟踪备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserTypeName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string CreateByUserName { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string CreateByUserNamePhone { get; set; }
    }

    public class InGoods
    {
        public int CommodityId { get; set; }
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

        /// <summary>
        /// 良次品
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 应发数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int ConfirmQty { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

    }

    /// <summary>
    /// 运输信息
    /// </summary>
    public class TransportInfo
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string TransportOrderCode { get; set; }

        /// <summary>
        /// 运单类型
        /// </summary>
        public int TransOrderType { get; set; }

        /// <summary>
        /// 运单类型名称
        /// </summary>
        public string TransOrderTypeName { get; set; }

        /// <summary>
        /// 运单状态
        /// </summary>
        public int TransOrderStatus { get; set; }

        /// <summary>
        /// 运单状态名称
        /// </summary>
        public string TransOrderStatusName { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 到达地
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// 提货时间
        /// </summary>
        public string PickDate { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public string SignDate { get; set; }

        /// <summary>
        /// 承运商
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 司机手机号
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 车辆牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型名称
        /// </summary>
        public string VehicleTypeName { get; set; }

        /// <summary>
        /// 车次号
        /// </summary>
        public string TransportCode { get; set; }

        /// <summary>
        /// 运输凭证
        /// </summary>
        public int TransPictureNum { get; set; }
    }
}
