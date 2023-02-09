using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqCommodityQuery
    {
        /// <summary>
        /// 仓库Id
        /// </summary>
        public long WarehouseId { get; set; }
        /// <summary>
        /// 货主Id（会员Id）
        /// </summary>
        public long CargoOwnerId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 良次品
        /// </summary>
        public int StockType { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public long CountNum { get; set; }
    }
}
