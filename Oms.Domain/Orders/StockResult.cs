namespace Oms.Domain.Orders
{
    public record HeldStockResult
    {
        public long OutBoundID { get; init; }
        public long DetailNumber { get; init; }
        public long ProductId { get; init; }
        public int HeldQty { get; init; }
    }
}
