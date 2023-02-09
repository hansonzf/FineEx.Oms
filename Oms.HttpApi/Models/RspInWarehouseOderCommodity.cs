using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspInWarehouseOderCommodity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
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


    }
}
