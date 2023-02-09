using System;
using System.Collections.Generic;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{

    /// <summary>
    /// 
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 客户/供应商编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 缩写
        /// </summary>
        public string SimpleName { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 统一社会信息编码
        /// </summary>
        public string SocialCode { get; set; }

        /// <summary>
        /// 客户所在具体地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }


        /// <summary>
        /// 开户银行
        /// </summary>
        public string DepositBank { get; set; }

        /// <summary>
        /// 开户银行账号
        /// </summary>
        public string BankNumber { get; set; }

        /// <summary>
        /// 银联号
        /// </summary>
        public string UnionPayNumber { get; set; }
        /// <summary>
        /// 银联号
        /// </summary>
        public List<int> ServiceTypeList { get; set; }
        public int TripleId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public List<ConsignerDto> CustomerConsigners { get; set; }

        /// <summary>
        /// 系统对接
        /// </summary>
        public List<SystemApplyDto> SystemApplys { get; set; }
    }
}
