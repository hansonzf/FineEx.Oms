using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqInWarehouseOrderSubmit
    {
        /// <summary>
        /// (必填)全球唯一ID，系统交互使用,下游交互ID
        /// </summary>
        public long UpSyncID { get; set; }
        /// <summary>
        /// (必填)会员id
        /// </summary>
        public int MemberID { get; set; }
        /// <summary>
        /// (必填)操作时间
        /// </summary>
        public string OperationDate { get; set; }
        /// <summary>
        /// (必填)操作人
        /// </summary>
        public int Operater { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperaterName { get; set; }
    }
}
