using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqTransPlan
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int ReceiptType { get; set; }
    }
}
