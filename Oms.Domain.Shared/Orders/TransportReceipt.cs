using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Domain.Orders
{
    public class TransportReceipt
    {
        public int Index { get; set; }
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
        /// 出发地ID
        /// </summary>
        public string FromAddressId { get; set; }

        /// <summary>
        /// 出发地名称
        /// </summary>
        public string FromAddressName { get; set; }
        
        /// <summary>
        /// 到达地ID
        /// </summary>
        public string ToAddressId { get; set; }

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
        /// 承运商Id
        /// </summary>
        public string CarrierId { get; set; }
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
}
