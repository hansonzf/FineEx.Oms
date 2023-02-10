using System.Security.Cryptography;
using System.Text;
using Volo.Abp;

namespace Oms.Domain.Orders
{
    public class OutboundOrder : BusinessOrder
    {
        public long OutboundId { get; private set; }
        public CustomerDescription Customer { get; private set; }
        public CargoOwnerDescription CargoOwner { get; private set; }
        public AddressDescription DeliveryInfo { get; private set; }
        public DeliveryTypes DeliveryType { get; private set; }
        public OutboundTypes OutboundType { get; private set; }
        public WarehouseDescription Warehouse { get; private set; }
        public string? Remark { get; private set; }
        public string CombinationCode { get; private set; }
        public DateTime ExpectingOutboundTime { get; private set; }
        public DateTime? FactOutboundTime { get; private set; }
        public List<CheckoutProduct> Details { get; protected set; }

        protected override bool IsCombinationSupport => true;
        protected override bool IsNeedToCheckInventory => true;
        protected override bool IsNeedToMatchTransport => true;

        protected OutboundOrder()
            : base()
        { }

        public OutboundOrder(Guid id, long outboundId, string orderNumber, string externalOrderNumber, string tenantId, CustomerDescription customer, CargoOwnerDescription owner, AddressDescription deliveryInfo, DeliveryTypes deliveryType, OutboundTypes outboundType, WarehouseDescription warehouse, DateTime expectingOutboundTime, DateTime expectingCompleteTime)
            : base(id, tenantId, orderNumber, externalOrderNumber, BusinessTypes.OutboundWithTransport, expectingCompleteTime)
        {
            OutboundId = outboundId;
            Customer = customer;
            CargoOwner = owner;
            DeliveryInfo = deliveryInfo;
            DeliveryType = deliveryType;
            OutboundType = outboundType;
            Warehouse = warehouse;
            ExpectingOutboundTime = expectingOutboundTime;
            Details = new List<CheckoutProduct>();
            CalculateCombinationCode();
        }

        public void CalculateCombinationCode()
        {
            if ((int)OrderState > 5)
                return;

            StringBuilder originalText = new StringBuilder();
            originalText.AppendFormat("{0}@", Customer.CustomerId);
            originalText.AppendFormat("{0}-{1}-{2}-{3}-{4}@", DeliveryInfo.Province, DeliveryInfo.City, DeliveryInfo.District, DeliveryInfo.Contact, DeliveryInfo.Phone);
            originalText.AppendFormat("{0}#", Warehouse.WarehouseId);
            originalText.Append(Enum.GetName(OrderState));

            byte[] content = Encoding.UTF8.GetBytes(originalText.ToString());
            byte[] bytes = SHA256.Create().ComputeHash(content);

            StringBuilder secretText = new();
            foreach (byte b in bytes)
                secretText.Append(b.ToString("x2"));

            CombinationCode = secretText.ToString();
        }

        public List<string> MergeOrders(params OutboundOrder[] orders)
        {
            if (!IsCombinationSupport)
                return new List<string>();

            if (orders is null || !orders.Any())
                throw new BusinessException(message: $"{nameof(orders)} is null or contains empty elements");
            if (orders.Length == 1)
                throw new BusinessException(message: $"{nameof(orders)} contains only one order");

            OutboundOrder masterOrder;
            int masterCount = orders.Count(o => o.RelationType == RelationTypes.CombinedMaster);
            if (masterCount > 1)
                throw new BusinessException(message: "There have more than one combination root order");
            // 从集合中选出合并主订单，如果不存在，则任选一个订单作为合并主订单
            masterOrder = orders.FirstOrDefault(o => o.RelationType == RelationTypes.CombinedMaster) ?? orders.First();
            masterOrder.RelationType = RelationTypes.CombinedMaster;
            masterOrder.Visible = true;

            foreach (var order in orders)
            {
                if (order.Id == masterOrder.Id)
                    continue;

                if (order == null)
                    throw new ArgumentException("The order type error when process outbound business");
                if (order.RelationType != RelationTypes.StandAlone)
                    throw new BusinessException(message: $"order {order.Id} relation type is {Enum.GetName(order.RelationType)}, there require StandAlone state");


                order.RelationType = RelationTypes.CombinedSlave;
                order.Visible = false;
                order.AddRelateOrderId(masterOrder.Id);
                masterOrder.AddRelateOrderId(order.Id);
            }

            return masterOrder.RelatedOrderIds;
        }

        public virtual List<string> UndoMergeOrders(params OutboundOrder[] orders)
        {
            if (orders is null || !orders.Any())
                throw new BusinessException(message: $"{nameof(orders)} is null or contains empty elements");

            foreach (var order in orders)
            {
                if (order.RelationType != RelationTypes.CombinedSlave || order.RelatedOrderIds.FirstOrDefault() != Id.ToString().ToLower())
                    throw new BusinessException(message: $"The order {order.Id} is not related to current master order {Id}");
                if (!TestOrderIdIsRelated(order.Id))
                    throw new BusinessException(message: $"The order {order.Id} is not related to current master order {Id}");

                if (order == null)
                    throw new ArgumentException("The order type error when process outbound business");

                order.RelationType = RelationTypes.StandAlone;
                order.Visible = true;
                order._relatedOrderIds = string.Empty;
                RemoveRelateOrderId(order.Id);
            }

            if (RelatedOrderIds.Count == 0)
            {
                RelationType = RelationTypes.StandAlone;
                Visible = true;
            }

            return RelatedOrderIds;
        }

        public void AddOrRepalceProducts(IEnumerable<CheckoutProduct> products)
        {
            if (products is null || !products.Any())
                throw new ArgumentNullException(nameof(products));


            if (!products.All(p => p.OrderId == Id))
                throw new ArgumentException($"all the products must link to order, set OrderId to {Id}");

            var list = new List<CheckoutProduct>(Details);
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
                var list = new List<CheckoutProduct>();
                foreach (var item in Details)
                {
                    if (detailIds.Any(d => detailIds.Contains(d)))
                        continue;
                    list.Add(item);
                }
                Details = list;
            }
        }









        public override void CheckInventory(bool automaticCheck = false)
        {
            base.CheckInventory(automaticCheck);
            CalculateCombinationCode();
        }

        public override void SetCheckedInventoryResult(IEnumerable<HeldStockResult> stockResult)
        {
            base.SetCheckedInventoryResult(stockResult);
            CalculateCombinationCode();
            foreach (var item in stockResult)
            {
                var detail = Details.SingleOrDefault(d => d.DetailNumber == item.DetailNumber);
                if (detail is null) continue;
                detail.HoldingStock(item.HeldQty);
            }
        }

        public override void MatchTransportStrategy(bool automaticMatch = false)
        {
            base.MatchTransportStrategy(automaticMatch);
            CalculateCombinationCode();
        }

        public override void SetMatchedResult(TransportStrategy strategy)
        {
            base.SetMatchedResult(strategy);
            CalculateCombinationCode();
        }

        public override void Cancel()
        {
            if ((int)OrderState > 5)
                throw new BusinessException(message: $"只能取消未开始分拣订单，当前订单状态为:[{Enum.GetName(OrderState)}]");

            AddLocalEvent(new CancelOrderEvent
            {
                OrderId = Id,
                BusinessType = BusinessType,
                OrderState = OrderState,
                IsNeedToCheckStock = IsNeedToCheckInventory,
                OutboundId = OutboundId,
                MemberID = CargoOwner.CargoOwnerId,
                CancelType = 2,
                CancelMemo = "手工取消",
                Operater = 0
            });
            OrderState = OrderStatus.Cancelled;
            CalculateCombinationCode();
        }

        public override void Dispatched()
        {
            base.Dispatched();
            CalculateCombinationCode();
        }

        public override void SuccessUndoDispatch()
        {
            base.SuccessUndoDispatch();
            CalculateCombinationCode();
        }
    }
}
