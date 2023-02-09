using System;
using System.Collections.Generic;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class RegionDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 供应商id
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 供应商id
        /// </summary>
        public List<RegionDto> Children { get; set; }
    }
}

