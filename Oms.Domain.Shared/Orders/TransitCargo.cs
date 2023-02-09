using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class TransitCargo : ValueObject
    {
        public Guid OrderId { get; private set; }
        public string CargoCode { get; private set; }
        public string CargoName { get; private set; }
        public int PackageCount { get; private set; }
        public int InnerPackage { get; private set; }
        public double Weight { get; private set; }
        public double Volume { get; private set; }
        public decimal Fee { get; private set; }

        private TransitCargo() 
        { }

        public TransitCargo(Guid orderId, string cargoCode, string cargoName, int packageCount, int innerPackage, double weight, double volume, decimal fee)
        {
            OrderId = orderId;
            CargoCode = cargoCode;
            CargoName = cargoName;
            PackageCount = packageCount;
            InnerPackage = innerPackage;
            Weight = weight;
            Volume = volume;
            Fee = fee;
        }

        public void BindToOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return OrderId;
            yield return CargoCode;
            yield return CargoName;
            yield return PackageCount;
            yield return InnerPackage;
            yield return Weight;
            yield return Volume;
            yield return Fee;
        }
    }
}
