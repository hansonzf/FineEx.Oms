using Oms.Domain.Orders;
using Volo.Abp.Application.Dtos;

namespace Oms.Application.Orders
{
    public class CheckinDetailDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public long DetailNumber { get; set; }
        public string ProductCode { get; set; }
        public string ProductBatch { get; set; }
        public StockTypes StockType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int Qty { get; set; }
        public int HoldingQty { get; set; }
    }
}
