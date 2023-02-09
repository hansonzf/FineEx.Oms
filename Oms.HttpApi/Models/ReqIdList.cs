using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqIdList
    {
        /// <summary>
        /// Id集合
        /// </summary>
        public List<long> OrderIds { get; set; }
    }
}
