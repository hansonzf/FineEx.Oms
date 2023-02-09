using Oms.Domain.Orders;
using Volo.Abp.Application.Dtos;

namespace Oms.Application.Orders
{
    public class CombineDetailDto : EntityDto
    {
        public string OrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public IEnumerable<CombinedProductDto> Details { get; set; }
    }

    public class CombinedProductDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string ProductBatch { get; set; }
        public StockTypes StockType { get; set; }
        public int RequiredQty { get; set; }
    }
}
