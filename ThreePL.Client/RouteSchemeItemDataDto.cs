using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteSchemeItemDataDto
    {

        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 线路方案id
        /// </summary>
        public Guid RouteSchemeId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 运力/中转
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 运力/中转id
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 运力/中转名称
        /// </summary>
        public string Name { get; set; }

    }
}

