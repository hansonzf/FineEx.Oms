using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class WarehouseProcessing : ValueObject
    {
        public string? WarehouseOrderId { get; private set; }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
