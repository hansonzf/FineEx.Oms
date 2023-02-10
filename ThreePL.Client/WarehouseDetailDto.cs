using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePL.Client
{
    public class WarehouseDetailDto
    {
        public string Id { get; set; }
        public int TripleId { get; set; }
        public string TenantId { get; set; }
        public string SupplierId { get; set; }
        public int SupplierName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int AddressType { get; set; }
        public bool IsEnabled { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public string Memo { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
