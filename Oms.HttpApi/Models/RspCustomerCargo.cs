using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspCustomerCargo
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 货主列表
        /// </summary>
        public List<RspCargoOwner> CargoOwnerList { get; set; }

    }

    public class RspCargoOwner
    {
        /// <summary>
        /// 货主Id
        /// </summary>
        public long CargoOwnerId { get; set; }
        /// <summary>
        /// 货主名称
        /// </summary>
        public string CargoOwnerName { get; set; }


    }
}
