using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class CheckinProduct : ValueObject
    {
        public long DetailNumber { get; private set; }
        public Guid OrderId { get; protected set; }
        public int ProductId { get; private set; }
        public string ProductBatch { get; private set; }
        public string SKU { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public StockTypes StockType { get; private set;  }
        public int Qty { get; private set; }
        public int ConfirmedQty { get; private set; }
        public double Volume { get; private set; }
        public double Weight { get; private set; }

        protected CheckinProduct() { }

        public CheckinProduct(Guid orderId, long detailNumber, int productId, string sKU, string productBatch, string productCode, string productName, StockTypes stockType, int qty)
        {
            OrderId = orderId;
            DetailNumber = detailNumber;
            ProductId = productId;
            SKU = sKU;
            ProductBatch = productBatch;
            ProductCode = productCode;
            ProductName = productName;
            StockType = stockType;
            Qty = qty;
        }

        public void BindToOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public void SetConfirmedQty(int qty)
        {
            ConfirmedQty = qty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
