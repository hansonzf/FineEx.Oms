using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class TransportDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 供应商id
        /// </summary>
        public Guid? SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 合作方式
        /// </summary>
        public int? Mode { get; set; }

        /// <summary>
        /// 运力名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 运力类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 归属类型
        /// </summary>
        public int AttributionType { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string LastModificationName { get; set; }


    }
}

