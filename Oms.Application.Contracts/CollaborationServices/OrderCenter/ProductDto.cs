namespace Oms.Application.Contracts.CollaborationServices.OrderCenter
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBatch { get; set; }
    }
}
