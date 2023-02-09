using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Values;

namespace Oms.Domain.Orders
{
    public class TransportStrategy : ValueObject
    {
        private List<TransportResource> _resources;
        private List<TransportReceipt> _transports;
        public string MatchedTransportLineName { get; private set; }
        public int MatchType { get; private set; }
        public string Memo { get; private set; }
        public string TransportLine { get; private set; }
        public string TransportReceipts { get; private set; }


        private TransportStrategy() 
        {
            _resources = new List<TransportResource>();
            _transports = new List<TransportReceipt>();
        }

        public TransportStrategy(string matchedName, string memo, int matchType, List<TransportResource> transportResources)
            : this()
        {
            MatchedTransportLineName = matchedName;
            MatchType = matchType;
            Memo = memo;
            if (transportResources is not null && transportResources.Any())
            {
                TransportLine = JsonConvert.SerializeObject(transportResources);
                TransportResources = transportResources.AsReadOnly();
                for (int i = 0; i < TransportResources.Count; i++)
                {
                    if (i > 0 && i % 2 == 0)
                    {
                        var segement = new TransportReceipt
                        {
                            Index = i / 2 + 1,
                            FromAddressId = TransportResources[i - 2].ResourceId,
                            FromAddressName = TransportResources[i - 2].Name,
                            ToAddressId = TransportResources[i].ResourceId,
                            ToAddressName = TransportResources[i].Name,
                            CarrierId = TransportResources[i - 1].ResourceId,
                            CarrierName = TransportResources[i - 1].Name,
                        };
                        _transports.Add(segement);
                    }
                }
                TransportReceipts = JsonConvert.SerializeObject(_transports);
            }
        }

        public ReadOnlyCollection<TransportResource> TransportResources
        {
            get
            {
                if (!_resources.Any() && !string.IsNullOrEmpty(TransportLine))
                {
                    _resources = JsonConvert.DeserializeObject<List<TransportResource>>(TransportLine) ?? new List<TransportResource>();
                }

                return _resources.AsReadOnly();
            }
            private set
            {
                _resources = new List<TransportResource>(value);
            }
        }

        public ReadOnlyCollection<TransportReceipt> TransportDetails
        {
            get
            {
                if (!_transports.Any() && !string.IsNullOrEmpty(TransportReceipts))
                {
                    _transports = JsonConvert.DeserializeObject<List<TransportReceipt>>(TransportReceipts) ?? new List<TransportReceipt>();
                }

                return _transports.AsReadOnly();
            }
            private set
            {
                _transports = new List<TransportReceipt>(value);
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return MatchedTransportLineName;
            yield return MatchType;
            yield return Memo;
            yield return TransportLine;
            yield return TransportReceipts;
        }
    }
}
