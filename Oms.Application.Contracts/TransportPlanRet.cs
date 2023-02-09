using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts
{
    /// <summary>
    /// 编辑运输方案回显返回值
    /// </summary>
    public class RspUpdatePlanInfo
    {
        /// <summary>
        /// 选中运输方案Id
        /// </summary>
        public string TransportPlanId { get; set; }

        /// <summary>
        /// 匹配运输方案
        /// </summary>
        public List<TransportPlanRet> MatchTransPlans { get; set; }

        /// <summary>
        /// 手动调度
        /// </summary>
        public TransportPlanRet NewTransPlan { get; set; }
    }

    /// <summary>
    /// 运输方案返回值
    /// </summary>
    public class TransportPlanRet
    {
        /// <summary>
        /// 运输方案Id
        /// </summary>
        public string TransportPlanId { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 运输方案明细
        /// </summary>
        public List<TransPlanDetail> TransPlanDetails { get; set; }
    }

    /// <summary>
    /// 运输方案明细
    /// </summary>
    public class TransPlanDetail
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 中转地Id
        /// </summary>
        public string LabelKey { get; set; } = "";

        /// <summary>
        /// 地点
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 运力资源Id
        /// </summary>
        public string TipKey { get; set; } = "";

        /// <summary>
        /// 运力资源
        /// </summary>
        public string Tip { get; set; }
    }
}
