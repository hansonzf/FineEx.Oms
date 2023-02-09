using Oms.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    /// <summary>
    /// 编辑运输方案回显 返回数据
    /// </summary>
    public class RspOmsUpdatePlanInfoQuery
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 选中运输方案Id
        /// </summary>
        public string TransportPlanId { get; set; }

        /// <summary>
        /// 运输方案详情
        /// </summary>
        public TransportPlanRet TransPlanInfo { get; set; }

    }
}
