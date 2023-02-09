using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqMergeOrder
    {
        /// <summary>
        /// 订单List
        /// </summary>
        public List<long> OrderIds { get; set; } = new List<long>();
    }
}
