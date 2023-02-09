using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqInCommodityQuery
    {
        /// <summary>
        /// 货主Id（会员Id）
        /// </summary>
        public long CargoOwnerId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string BarCode { get; set; }
    }
}
