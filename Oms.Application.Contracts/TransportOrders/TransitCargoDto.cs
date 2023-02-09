namespace Oms.Application.Orders
{
    public class TransitCargoDto
    {
        public Guid OrderId { get; set; }
        public string Code { get; set; }
        public string CargoName { get; set; }
        public int PackageCount { get; set; }
        public int InnerPackage { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public decimal Fee { get; set; }
    }
}
