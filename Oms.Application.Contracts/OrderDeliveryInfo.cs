using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts
{
    public class OrderDeliveryInfo
    {
        /// <summary>
        /// 订单Id
        /// </summary>              
        public long OrderId { get; set; } = 0;
        public int BusinessType { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 寄件单位
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 寄件地址Id
        /// </summary>
        public string FromAddressId { get; set; }
        /// <summary>
        /// 起始省
        /// </summary>
        public string FromProvince { get; set; }
        /// <summary>
        /// 起始市
        /// </summary>
        public string FromCity { get; set; }
        /// <summary>
        /// 起始县/区
        /// </summary>
        public string FromArea { get; set; }
        /// <summary>
        /// 收件单位
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 收件地址Id
        /// </summary>
        public string ToAddressId { get; set; }
        /// <summary>
        /// 目的省
        /// </summary>
        public string ToProvince { get; set; }
        /// <summary>
        /// 目的市
        /// </summary>
        public string ToCity { get; set; }
        /// <summary>
        /// 目的县/区
        /// </summary>
        public string ToArea { get; set; }
    }
}
