using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Orders
{
    public class OutboundQueryDto
    {
        public int TimeType { get; set; }
        public List<DateTime> TimeRange { get; set; }
        public int CustomerId { get; set; }
        public int ConsignorId { get; set; }
        public string CustomerOrderCode { get; set; }
        public string OrderCode { get; set; }
        public string TMSTransportCode { get; set; }
        public int OrderStatus { get; set; }
        public int StockAuditStatus { get; set; }
        public int WarehouseId { get; set; }
        public string WMSOutWarehouseCode { get; set; }
        public int DeliveryType { get; set; }
    }
}
