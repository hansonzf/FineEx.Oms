using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePL.Client
{
    /// <summary>
    /// 运输服务商类型枚举
    /// </summary>
    public enum CarrierTypeEnum
    {
        [Description("承运商")]
        Transport = 1,

        [Description("网点")]
        NetWork = 2,
    }

    /// <summary>
    /// 起点终点类型枚举
    /// </summary>
    public enum ModeTypeEnum
    {
        [Description("起点")]
        Start = 1,

        [Description("终点")]
        End = 2,
    }

    /// <summary>
    /// 起始到达地类型枚举
    /// </summary>
    public enum LocationTypeEnum
    {
        [Description("按收发地")]
        ByAddress = 1,

        [Description("按省市区")]
        ByCity = 2,
    }
}
