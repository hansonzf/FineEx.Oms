using Oms.Application.Orders;
using Oms.Domain.Orders;
using System.ComponentModel.DataAnnotations;

namespace Oms.HttpApi.Models.Orders
{
    public class MatchTransportLinePayload
    {
        [Required]
        public string TransportLineName { get; set; }
        public int MatchType { get; set; }
        public List<TransportResource> TransportResources { get; set; }
    }
}
