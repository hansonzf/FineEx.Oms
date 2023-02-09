using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqCommodityFileQuery
    {
        /// <summary>
        /// 仓库Id
        /// </summary>
        public long WarehouseId { get; set; }
        /// <summary>
        /// 货主Id（会员Id）
        /// </summary>
        public long CargoOwnerId { get; set; }
    }
}
