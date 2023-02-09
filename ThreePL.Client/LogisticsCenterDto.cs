using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class LogisticsCenterDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 供应商id
        /// </summary>
        public string SupplierId { get; set; }

        /// <summary>
        /// 集散中心编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 集散中心名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 集散中心类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}

