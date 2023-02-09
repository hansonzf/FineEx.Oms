using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class WarehouseDescription : ValueObject
    {
        public int WarehouseId { get; set; }
        public int InterfaceWarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        private WarehouseDescription() { }

        public WarehouseDescription(int warehouseId, int interfaceWarehouseId, string warehouseName)
        {
            WarehouseId = warehouseId;
            InterfaceWarehouseId = interfaceWarehouseId;
            WarehouseName = warehouseName ?? string.Empty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return WarehouseId;
            yield return InterfaceWarehouseId;
            yield return WarehouseName;
        }
    }
}
