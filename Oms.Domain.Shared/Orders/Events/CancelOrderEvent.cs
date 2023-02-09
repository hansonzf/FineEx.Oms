namespace Oms.Domain.Orders
{
    public class CancelOrderEvent
    {
        public Guid OrderId { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public OrderStatus OrderState { get; set; }
        public bool IsNeedToCheckStock { get; set; }
        public long OutboundId { get; set; }
        public int MemberID { get; set; }
        public int CancelType { get; set; }
        public int Operater { get; set; }
        public string CancelMemo { get; set; }
    }
}
