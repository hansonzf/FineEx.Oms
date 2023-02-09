namespace Oms.HttpApi.Models.Orders
{
    public class OrderIdListPayload
    {
        public List<Guid> OrderIds { get; set; }
        public int OrderIdsCount => OrderIds == null ? 0 : OrderIds.Count;
    }
}
