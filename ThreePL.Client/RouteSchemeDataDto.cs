using System;
using System.Collections.Generic;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteSchemeDataDto
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 运力中转信息
        /// </summary>
        public List<RouteSchemeItemDataDto> Items { get; set; }

        /// <summary>
        /// 起始，终点地址信息
        /// </summary>
        public List<RouteSchemeLocationDto> Locations { get; set; }


    }
}

