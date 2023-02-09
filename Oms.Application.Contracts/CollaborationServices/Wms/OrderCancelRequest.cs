using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.CollaborationServices.Wms
{
    public class OrderCancelRequest
    {
        /// <summary>
		/// (必填)全球唯一ID，系统交互使用
		/// </summary>
		public long UpSyncID { get; set; }

        /// <summary>
        /// (必填)会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// (必填)单据取消类型 1 订单、2 出库单、3 入库单、4 退货单、5 加工单 6.调拨单7.盘点单 9.调整单 10 加工需求单
        /// </summary>
        public int CancelType { get; set; }

        /// <summary>
        /// 取消方式 1 异步取消，2 同步取消 3 转发同步取消
        /// </summary> 
        public int CancelMode { get; set; }

        /// <summary>
        /// (必填)取消备注
        /// </summary>
        public string CancelMemo { get; set; }

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
