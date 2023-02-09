using System.Text.RegularExpressions;
using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class AddressDescription : ValueObject
    {
        public string AddressId { get; private set; }
        public string Contact { get; private set; }
        public string Phone { get; private set; }
        public string AddressName { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string District { get; private set; }
        public string Address { get; private set; }

        private AddressDescription()
        { }

        public AddressDescription(string province, string city, string district, string detailAddress)
        {
            Province = province;
            City = city;
            District = district;
            Address = detailAddress;
        }

        public AddressDescription(string addressId, string contact, string phone, string addressName, string province, string city, string district, string detailAddress)
        {
            AddressId = addressId;
            Contact = contact;
            Phone = phone;
            AddressName = addressName;
            Province = province;
            City = city;
            District = district;
            Address = detailAddress;
        }

        public bool IsValid()
        {
            bool result = true;
            result &= !string.IsNullOrEmpty(Contact);
            result &= !string.IsNullOrEmpty(Phone);
            result &= !string.IsNullOrEmpty(AddressName);
            result &= Regex.IsMatch(Phone, @"^(1)\d{10}$");
            result &= !string.IsNullOrEmpty(Province);
            result &= !string.IsNullOrEmpty(City);
            result &= !string.IsNullOrEmpty(Address);

            return result;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Contact;
            yield return Phone;
            yield return AddressName;
            yield return Province;
            yield return City;
            yield return District;
            yield return Address;
        }
    }
}
