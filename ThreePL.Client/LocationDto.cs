using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 
    /// </summary>
    public class LocationDto
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址类型
        /// </summary>
        public  int AddressType { get;  set; }

        /// <summary>
        /// 仓库，地址id
        /// </summary>
        public Guid? Key { get; set; }

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
        public string DetailAddress { get; set; }

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

