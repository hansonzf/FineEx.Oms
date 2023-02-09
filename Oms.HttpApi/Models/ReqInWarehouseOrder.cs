using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqInWarehouseOrder
    {
        /// <summary>
        ///  1表示出库单，2表示入库单，3表示运输单
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 需要下发的订单IdList
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

        /// <summary>
        /// (必填)操作人名称
        /// </summary>
        public string OperaterName { get; set; }
    }
}
