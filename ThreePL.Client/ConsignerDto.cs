using System;
using System.Collections.Generic;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    ///  货主输出模型
    /// </summary>
    public class ConsignerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 货主编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 货主名称
        /// </summary>
        public string Name { get; set; }

        public int CustomerTripleId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string ContactAddress { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 三方系统Id
        /// </summary>
        public int? TripleId { get; set; }

        /// <summary>
        /// 是否启用的
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 货主所属组
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
