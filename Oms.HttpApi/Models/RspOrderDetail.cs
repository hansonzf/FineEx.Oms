using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    /// <summary>
    /// 运单详情
    /// </summary>
    public class RspOrderDetail
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; } = 0;
        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string SyncOrderCode { get; set; } = "";
        /// <summary>
        /// 运单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 运单状态名称
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomerId { get; set; } = 0;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 发货联系人姓名
        /// </summary>
        public string FromContacter { get; set; }
        /// <summary>
        /// 发货联系人手机号
        /// </summary>
        public string FromPhone { get; set; }
        /// <summary>
        /// 发货地址Id
        /// </summary>
        public string FromAddressId { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 收货联系人姓名
        /// </summary>
        public string ToContacter { get; set; }
        /// <summary>
        /// 收货联系人手机号
        /// </summary>
        public string ToPhone { get; set; }
        /// <summary>
        /// 收货地址Id
        /// </summary>
        public string ToAddressId { get; set; }
        /// <summary>
        /// 货物明细
        /// </summary>
        public List<ReqOrderGoods> OrderGoods { get; set; }
        /// <summary>
        /// 运单列表
        /// </summary>
        public List<TransOrder> TransOrders { get; set; }
        /// <summary>
        /// 运单轨迹
        /// </summary>
        public List<RspOrderTrack> OrderTrack { get; set; }
    }

    /// <summary>
    /// 订单详情的运单返回值
    /// </summary>
    public class TransOrder
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string TransOrderCode { get; set; }
        /// <summary>
        /// 运单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 运单类型名称
        /// </summary>
        public string OrderTypeName { get; set; }
        /// <summary>
        /// 运单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 运单状态名称
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        public string FromAddressName { get; set; }

        /// <summary>
        /// 到达地名称
        /// </summary>
        public string ToAddressName { get; set; }

        /// <summary>
        /// 提货时间
        /// </summary>
        public DateTime RealPickDate { get; set; }
        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime RealDeliveryDate { get; set; }
        /// <summary>
        /// 承运商名称
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// 司机姓名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 司机电话
        /// </summary>
        public string DriverPhone { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 车型名称
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

    /// <summary>
    /// 订单轨迹返回值
    /// </summary>
    public class RspOrderTrack
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatedName { get; set; }
    }
}
