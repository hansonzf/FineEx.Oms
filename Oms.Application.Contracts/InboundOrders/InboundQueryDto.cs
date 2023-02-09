using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.InboundOrders
{
    public class InboundQueryDto
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// 货主Id
        /// </summary>
        public long CargoOwnerId { get; set; }

        /// <summary>
        /// 客户外部单号
        /// </summary>
        public string CustomerOrderCode { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string TransOrderCode { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 入库仓库Id
        /// </summary>
        public long InWarehouseId { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string InWarehouseCode { get; set; }

        /// <summary>
        /// 分页页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }
    }
}
