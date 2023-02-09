using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspAddress
    {
        /// <summary>
        /// 仓库，地址id
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public string AddressName { get; set; }


        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacter { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

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
        public string Area { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }


    }
}
