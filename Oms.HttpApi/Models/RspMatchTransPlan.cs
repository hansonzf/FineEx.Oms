using Oms.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    /// <summary>
    /// 匹配运输方案返回值
    /// </summary>
    public class RspMatchTransPlan
    {
        /// <summary>
        /// 起始地址名称
        /// </summary>
        public string FromAddressName { get; set; }

        /// <summary>
        /// 终点地址名称
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 匹配运输方案
        /// </summary>
        public List<TransportPlanRet> MatchTransPlans { get; set; }
    }
}
