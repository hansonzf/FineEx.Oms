using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqOmsInOrderSendTOTms
    {
        /// <summary>
        /// 需要下发的订单Id
        /// </summary>
        public List<long> OrderIds { get; set; }
        /// <summary>
        /// (必填)操作时间
        /// </summary>
        public string OperationDate { get; set; }
        /// <summary>
        /// (必填)操作人
        /// </summary>
        public int Operater { get; set; }
    }
}
