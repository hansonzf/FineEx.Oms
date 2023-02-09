using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.TransportOrders
{
    public class TransportQueryDto
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public List<string> OrderCodeList { get; set; } = new List<string>();
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> SyncOrderCodeList { get; set; } = new List<string>();

        /// <summary>
        /// 日期类别（1=创建时间 2=提货时间 3=签收时间）
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 时间范围
        /// </summary>
        public List<DateTime> TimeRange { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 运单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 寄件单位
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 收件单位
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 订单单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 是否回单
        /// </summary>
        public int IsBack { get; set; }
    }
}
