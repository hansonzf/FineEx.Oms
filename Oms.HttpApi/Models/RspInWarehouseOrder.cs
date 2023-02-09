using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class RspInWarehouseOrder
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        public object Data { get; set; }
    }
}
