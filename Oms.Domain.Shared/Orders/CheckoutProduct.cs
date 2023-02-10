using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class CheckoutProduct : ValueObject
    {
        public Guid OrderId { get; private set; }
        public long DetailNumber { get; private set; }
        public int ProductId { get; private set; }
        public string SKU { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public string ProductBatch { get; private set; }
        public StockTypes StockType { get; private set; }
        public int RequiredQty { get; private set; }
        public int HoldingQty { get; private set; }
        public int FactQty { get; private set; }

        protected CheckoutProduct()
        { }

        public CheckoutProduct(Guid orderId, long detailNumber, int productId, string sKU, string productCode, string productName, string productBatch, StockTypes stockType, int requiredQty)
        {
            OrderId = orderId;
            DetailNumber = detailNumber;
            ProductId = productId;
            SKU = sKU;
            ProductCode = productCode;
            ProductName = productName;
            ProductBatch = productBatch;
            StockType = stockType;
            RequiredQty = requiredQty;
            HoldingQty = 0;
        }

        public void BindToOrder(Guid orderId)
        {
            DetailNumber = DateTime.Now.Ticks;
            OrderId = orderId;
        }

        public void HoldingStock(int holdingQty)
        {
            HoldingQty = holdingQty;
        }

        public void ReleaseStock()
        {
            HoldingQty = 0;
        }

        public void SetFactQty(int factQty)
        {
            FactQty = factQty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return OrderId;
            yield return SKU;
            yield return DetailNumber;
            yield return ProductId;
            yield return ProductBatch;
            yield return StockType;
            yield return RequiredQty;
            yield return HoldingQty;
            yield return FactQty;
        }
    }
}
