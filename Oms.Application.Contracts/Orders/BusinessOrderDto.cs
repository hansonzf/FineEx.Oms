using Oms.Application.Contracts;
using Oms.Domain.Orders;
using Volo.Abp.Application.Dtos;

namespace Oms.Application.Orders
{
    public class BusinessOrderDto : EntityDto<Guid>
    {
        public string TenantId { get; set; }
        public string OrderNumber { get; set; }
        public string ExternalOrderNumber { get; set; }
        public int OrderSource { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public DateTime ReceivedAt { get; set; }
        public bool Visible { get; set; }
        public RelationTypes RelationType { get; set; }
        public OrderStatus OrderState { get; set; }
        public List<string> RelatedOrderIds { get; set; }
        public DateTime ExpectingCompleteTime { get; set; }
        public DateTime? FactCompleteTime { get; set; }
        public string MatchedTransportLineName { get; set; }
        public int MatchType { get; set; }
        public string? TransportStrategyId { get; set; }
        public string? TransportStrategyMemo { get; set; }
        public IEnumerable<TransportResource> TransportLineResources { get; set; }
        public List<TransportReceipt> TransportDetails { get; set; }
    }    
}
