using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteSchemeLocationDto
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
        /// 始发地/收货地
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 按地址/按省市区
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 地址id
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

    }
}

