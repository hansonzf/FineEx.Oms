using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class Goods
    {
        /// <summary>
        /// 商品id
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

        /// <summary>
        /// 批次编码
        /// </summary>
        public string? BatchCode { get; set; }

        ///// <summary>
        ///// 良次品
        ///// </summary>
        //public int ProductType { get; set; }
        /// <summary>
        /// 良次品
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volumn { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
    }
}
