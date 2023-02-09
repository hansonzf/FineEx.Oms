using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class CustomerDescription : ValueObject
    {
        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;

        private CustomerDescription() { }

        public CustomerDescription(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CustomerId;
            yield return CustomerName;
        }
    }
}
