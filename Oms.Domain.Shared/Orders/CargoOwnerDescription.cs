using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class CargoOwnerDescription : ValueObject
    {
        public int CargoOwnerId { get; private set; }
        public string CargoOwnerName { get; private set; }

        private CargoOwnerDescription() 
        { }

        public CargoOwnerDescription(int cargoOwnerId, string cargoOwnerName)
        {
            CargoOwnerId = cargoOwnerId;
            CargoOwnerName = cargoOwnerName ?? string.Empty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CargoOwnerId;
            yield return CargoOwnerName;
        }
    }
}
