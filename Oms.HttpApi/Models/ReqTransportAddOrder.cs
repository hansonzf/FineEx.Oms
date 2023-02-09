using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    /// <summary>
    /// 开单请求实体类
    /// </summary>
    public class ReqTransportAddOrder
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; } = 0;
        /// <summary>
        /// 业务类型
        /// </summary>
        public int OperateType { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string SyncOrderCode { get; set; } = "";
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; } = "";
        /// <summary>
        /// 寄件地址
        /// </summary>
        public string FromStopId { get; set; }
        /// <summary>
        /// 起始省
        /// </summary>
        public string FromProvince { get; set; }
        /// <summary>
        /// 起始市
        /// </summary>
        public string FromCity { get; set; }
        /// <summary>
        /// 起始区
        /// </summary>
        public string FromArea { get; set; }
        /// <summary>
        /// 起始详细地址
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 起始地址名称
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 寄件联系人
        /// </summary>
        public string FromContacter { get; set; }
        /// <summary>
        /// 寄件人手机号
        /// </summary>
        public string FromPhone { get; set; }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string ToStopId { get; set; }
        /// <summary>
        /// 目的省
        /// </summary>
        public string ToProvince { get; set; }
        /// <summary>
        /// 目的市
        /// </summary>
        public string ToCity { get; set; }
        /// <summary>
        /// 目的区
        /// </summary>
        public string ToArea { get; set; }
        /// <summary>
        /// 目的详细地址
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 目的地址名称
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 收件联系人
        /// </summary>
        public string ToContacter { get; set; }
        /// <summary>
        /// 收件人联系手机号
        /// </summary>
        public string ToPhone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";
        /// <summary>
        /// 预计提货时间
        /// </summary>
        public string PlanPickDate { get; set; }
        /// <summary>
        /// 期望送达时间
        /// </summary>
        public string PlanDeliveryDate { get; set; }

        /// <summary>
        /// 货物信息列表
        /// </summary>
        public List<ReqOrderGoods> OrderGoods { get; set; }
    }

    /// <summary>
    /// 订单货物实体类
    /// </summary>
    public class ReqOrderGoods
    {
        /// <summary>
        /// 货物码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string LoadName { get; set; }
        /// <summary>
        /// 货物数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 货物体积
        /// </summary>
        public decimal Volume { get; set; } = 0;
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal Weight { get; set; } = 0;
        /// <summary>
        /// 货物内件数
        /// </summary>
        public int DoubleAmount { get; set; } = 0;
    }
}
