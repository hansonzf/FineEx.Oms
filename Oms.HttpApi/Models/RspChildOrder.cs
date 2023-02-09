using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspChildOrder
    {
        /// <summary>
        /// 出库订单id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string CustomerOrderCode { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<Goods> CommodityList { get; set; }


    }
}
