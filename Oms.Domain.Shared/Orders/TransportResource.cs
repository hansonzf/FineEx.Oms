using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum TransportResourceTypes
    {
        [Description("承运商")]
        Vendor = 1,
        [Description("集散中心")]
        LogisticsCenter = 2
    }

    public class TransportResource
    {
        public int Index { get; set; }
        public string ResourceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public TransportResourceTypes Type { get; set; }
        public string Contact { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
