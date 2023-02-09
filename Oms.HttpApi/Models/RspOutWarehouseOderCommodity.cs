using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspOutWarehouseOderCommodity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string Barcode { get; set; }
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
        /// 良次品类别(1=良次品)
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 良次品类别(1=良次品)
        /// </summary>
        public string StockTypeName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long Qty { get; set; }
    }
}
