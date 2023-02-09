using Newtonsoft.Json;
using Volo.Abp;

namespace Oms.Domain.Orders
{
    public class InboundOrder : BusinessOrder
    {
        List<CheckinProduct> _details = new List<CheckinProduct>();
        public long InboundId { get; private set; }
        public CustomerDescription Customer { get; private set; }
        public CargoOwnerDescription CargoOwner { get; private set; }
        public AddressDescription DeliveryInfo { get; private set; }
        public bool IsReturnOrder { get; private set; }
        public string? OriginDeliveryNumber { get; private set; }
        public WarehouseDescription Warehouse { get; private set; }
        public string? Remark { get; private set; }
        public InboundTypes InboundType { get; private set; }

        protected override bool IsCombinationSupport => false;
        protected override bool IsNeedToCheckInventory => true;
        protected override bool IsNeedToMatchTransport => true;

        protected InboundOrder()
        { }

        public InboundOrder(
            Guid id,
            long inboundId,
            string tenantId,
            string orderNumber,
            string externalOrderNumber,
            CustomerDescription customer,
            CargoOwnerDescription owner,
            AddressDescription deliveryInfo,
            WarehouseDescription warehouse,
            bool isReturnOrder,
            string originOrderNumber,
            InboundTypes inboundType,
            DateTime expectingCompleteTime,
            string remark) : base(id, tenantId, orderNumber, externalOrderNumber, BusinessTypes.InboundWithTransport, expectingCompleteTime)
        {
            InboundId = inboundId;
            Customer = customer;
            CargoOwner = owner;
            DeliveryInfo = deliveryInfo;
            Warehouse = warehouse;
            IsReturnOrder = isReturnOrder;
            OriginDeliveryNumber = originOrderNumber;
            Remark = remark;
            InboundType = inboundType;
        }

        public List<CheckinProduct> Details
        {
            get
            {
                if (!_details.Any() && !string.IsNullOrEmpty(_orderDetails))
                {
                    _details = JsonConvert.DeserializeObject<List<CheckinProduct>>(_orderDetails) ?? new List<CheckinProduct>();
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

        public void AddOrRepalceProducts(IEnumerable<CheckinProduct> products)
        {
            if (products is null || !products.Any())
                throw new ArgumentNullException(nameof(products));


            if (!products.All(p => p.OrderId == Id))
                throw new ArgumentException($"all the products must link to order, set OrderId to {Id}");

            var list = new List<CheckinProduct>(Details);
            foreach (var product in products)
            {
                var p = Details.FirstOrDefault(x => x.DetailNumber == product.DetailNumber && x.DetailNumber != 0);
                if (p is not null)
                    p = product;
                else
                    list.Add(product); 
            }
            Details = list;
        }

        public void RemoveProducts(params long[] detailIds)
        {
            if (detailIds.Any())
            {
                var list = new List<CheckinProduct>();
                foreach (var item in Details)
                {
                    if (detailIds.Any(d => detailIds.Contains(d)))
                        continue;
                    list.Add(item);
                }
                Details = list;
            }
        }

        public void Update(DateTime expectingCompleteTime, bool isReturnOrder, string remark, string originDeliveryNumber, CustomerDescription customer, CargoOwnerDescription cargoOwner, AddressDescription deliveryInfo, WarehouseDescription warehouse)
        {
            if ((int)OrderState > 0)
                throw new BusinessException(message: "已提交的订单无法修改");

            ExpectingCompleteTime = expectingCompleteTime;
            Customer = customer;
            CargoOwner = cargoOwner;
            DeliveryInfo = deliveryInfo;
            Warehouse = warehouse;
            IsReturnOrder = isReturnOrder;
            OriginDeliveryNumber = originDeliveryNumber;
            Remark = remark;
        }

        public override void Cancel()
        {
            AddLocalEvent(new CancelOrderEvent
            {
                OrderId = Id,
                BusinessType = BusinessType,
                OrderState = OrderState,
                IsNeedToCheckStock = IsNeedToCheckInventory,
                OutboundId = InboundId,
                MemberID = CargoOwner.CargoOwnerId,
                CancelType = 3,
                CancelMemo = "手工取消",
                Operater = 0
            });
            base.Cancel();
        }

        protected override void WithdrawDispatchCore()
        {
            throw new NotImplementedException();
        }
    }
}
