using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Domain.Orders
{
    public class TransportResource
    {
        public int Index { get; set; }
        public string ResourceId { get; set; }
        public string Name { get; set; }
        //1 = 运力资源，2 =集散中心
        public int Type { get; set; }
        public string Contact { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
