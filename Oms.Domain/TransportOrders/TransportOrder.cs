using Newtonsoft.Json;
using Volo.Abp;

namespace Oms.Domain.Orders
{
    public class TransportOrder : BusinessOrder
    {
        List<TransitCargo> _details = new List<TransitCargo>();
        public long TransportId { get; private set; }
        public bool IsReturnBack { get; private set; }
        public TransportTypes TransportType { get; private set; }
        public CustomerDescription Customer { get; private set; }
        public AddressDescription SenderInfo { get; private set; }
        public AddressDescription ReceiverInfo { get; private set; }
        public string? Remark { get; private set; }
        public ConsignStatus ConsignState { get; private set; }
        public DateTime ExpectingPickupTime { get; private set; }
        public DateTime? FactPickupTime { get; private set; }

        protected TransportOrder() 
        { }

        public TransportOrder(
            Guid id,
            long transportId,
            string orderNumber,
            string tenantId,
            string externalOrderNumber,
            bool isReturnBack,
            TransportTypes transportType, 
            CustomerDescription customer,  
            AddressDescription senderInfo, 
            AddressDescription receiverInfo,
            DateTime expectingPickupTime,
            DateTime expectingCompleteTime,
            string remark)
            : base(id, tenantId, orderNumber, externalOrderNumber, BusinessTypes.Transport, expectingCompleteTime)
        {
            TransportType = transportType;
            IsReturnBack = isReturnBack;
            TransportId = transportId;
            Customer = customer;
            ExternalOrderNumber = externalOrderNumber;
            SenderInfo = senderInfo;
            ReceiverInfo = receiverInfo;
            Remark = remark;
            ExpectingPickupTime = expectingPickupTime;
            ConsignState = ConsignStatus.None;
        }

        protected override bool IsCombinationSupport => false;
        protected override bool IsNeedToCheckInventory => false;
        protected override bool IsNeedToMatchTransport => true;

        public List<TransitCargo> Details
        {
            get
            {
                if (!_details.Any() && !string.IsNullOrEmpty(_orderDetails))
                {
                    _details = JsonConvert.DeserializeObject<List<TransitCargo>>(_orderDetails) ?? new List<TransitCargo>();
                }

                return _details;
            }
            private set
            {
                if (value is null)
                {
                    _orderDetails = String.Empty;
                    _details.Clear();
                }
                else
                {
                    _orderDetails = JsonConvert.SerializeObject(value);
                    _details = value;
                }
            }
        }

        public void AddOrRepalceCargos(IEnumerable<TransitCargo> cargoList)
        {
            if (cargoList is null || !cargoList.Any())
                throw new ArgumentException(nameof(cargoList));

            var newDetail = new List<TransitCargo>(Details);
            foreach (var item in cargoList)
            {
                if (newDetail.Any(d => d.CargoName == item.CargoName))
                {
                    var detail = newDetail.FirstOrDefault(d => d.CargoName == item.CargoName);
                    detail = item;
                }
                else
                {
                    newDetail.Add(item);
                }
            }
            Details = newDetail;
        }

        public TransportOrder RemoveCargo(params string[] cargoNames)
        {
            if (cargoNames is null || !cargoNames.Any())
                throw new ArgumentNullException(nameof(cargoNames));
                
            var newDetail = new List<TransitCargo>();
            foreach (var item in Details)
            {
                if (cargoNames.Contains(item.CargoName))
                    continue;
                newDetail.Add(item);
            }
            Details = newDetail;

            return this;
        }

        public TransportOrder UpdateInformation(
            TransportTypes? transportType,
            string remark,
            DateTime? expectingPickupTime, 
            DateTime? expectingCompleteTime,
            CustomerDescription customer,
            AddressDescription sender,
            AddressDescription receiver)
        {
            if (OrderState == OrderStatus.Created)
            {
                TransportType = transportType ?? TransportType;
                ExpectingPickupTime = expectingPickupTime ?? ExpectingPickupTime;
                ExpectingCompleteTime = expectingCompleteTime ?? ExpectingCompleteTime;
                Remark = remark == null ? Remark : remark;
                Customer = customer;
                SenderInfo = sender;
                ReceiverInfo = receiver;
            }

            return this;
        }

        public TransportOrder UpdateTransportInfo(AddressDescription? senderInfo, AddressDescription? receiverInfo)
        {
            if (OrderState == OrderStatus.Created)
            {
                SenderInfo = senderInfo ?? SenderInfo;
                ReceiverInfo = receiverInfo ?? ReceiverInfo;
            }

            return this;
        }

        public TransportOrder UpdateDetails(List<TransitCargo> details)
        {
            if (OrderState == OrderStatus.Created)
            {
                Details = new List<TransitCargo>();
                AddOrRepalceCargos(details);
            }

            return this;
        }

        public override void SetMatchedResult(TransportStrategy strategy)
        {
            if (strategy.TransportResources.First().ResourceId != SenderInfo.AddressId)
                throw new BusinessException(message: "The transport strategy start location doesn't match order");
            if (strategy.TransportResources.Last().ResourceId != ReceiverInfo.AddressId)
                throw new BusinessException(message: "The trsnsport strategy end location doesn't match order");
            base.SetMatchedResult(strategy);
        }
    }
}
