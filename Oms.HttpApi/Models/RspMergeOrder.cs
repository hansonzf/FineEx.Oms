using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspMergeOrder
    {
        /// <summary>
        /// 发货单位
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 收获地址
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderCout { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public List<long> OrderIds { get; set; }
    }
}
