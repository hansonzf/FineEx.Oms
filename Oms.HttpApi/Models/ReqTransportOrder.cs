using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqTransportOrder
    {
        /// <summary>
        /// 订单List
        /// </summary>
        public List<long> OrderIds { get; set; }
    }
}
