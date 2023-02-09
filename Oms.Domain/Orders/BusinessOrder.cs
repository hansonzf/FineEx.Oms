using Oms.Domain.Shared.Orders.Events;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Oms.Domain.Orders
{
    public abstract class BusinessOrder : BasicAggregateRoot<Guid>
    {
        protected string? _relatedOrderIds;
        protected string? _orderDetails;
        public string OrderNumber { get; protected set; }
        public string ExternalOrderNumber { get; protected set; }
        public string TenantId { get; protected set; }
        public int OrderSource { get; protected set; }
        public BusinessTypes BusinessType { get; protected set; }
        public DateTime ReceivedAt { get; protected set; }
        public bool Visible { get; protected set; }
        public RelationTypes RelationType { get; protected set; }
        public OrderStatus OrderState { get; protected set; }
        public TransportStrategy? MatchedTransportStrategy { get; protected set; }
        public DateTime ExpectingCompleteTime { get; protected set; }
        public DateTime? FactCompleteTime { get; protected set; }

        public List<string> RelatedOrderIds
        {
            get => string.IsNullOrEmpty(_relatedOrderIds) ? new List<string>() : _relatedOrderIds.Split(',').ToList();
            protected set => _relatedOrderIds = value.Any() ? value.Aggregate((l, r) => $"{l},{r}") : string.Empty;
        }

        protected BusinessOrder() 
        { }

        public BusinessOrder(Guid id, string tenantId, string orderNumber, string externalOrderNumber, BusinessTypes businessType, DateTime expectingCompleteTime)
        {
            Id = id;
            TenantId = tenantId ?? string.Empty;
            OrderNumber = orderNumber;
            ExternalOrderNumber = externalOrderNumber;
            BusinessType = businessType;
            ReceivedAt = DateTime.Now;
            RelationType = RelationTypes.StandAlone;
            Visible = true;
            OrderState = OrderStatus.Created;
            ExpectingCompleteTime = expectingCompleteTime;

            AddDistributedEvent(new ReceivedNewOrderEvent
            {
                OrderId = Id,
                BusinessType = BusinessType
            });
        }

        protected virtual void AddRelateOrderId(Guid orderId)
        {
            if (RelatedOrderIds.Any(id => id == orderId.ToString().ToLower()))
                return;

            var list = new List<string> { orderId.ToString().ToLower() };
            list.AddRange(RelatedOrderIds);
            RelatedOrderIds = list;
            _relatedOrderIds = RelatedOrderIds.Aggregate((l, r) => $"{l},{r}");
        }

        protected virtual void RemoveRelateOrderId(Guid orderId)
        {
            var list = new List<string>(RelatedOrderIds);
            if (!RelatedOrderIds.Any(id => id == orderId.ToString().ToLower()))
                throw new BusinessException(message: "The specific orderId doesn't exist");

            if (list.Remove(orderId.ToString().ToLower()))
            {
                RelatedOrderIds = list;
                _relatedOrderIds = RelatedOrderIds.Aggregate((l, r) => $"{l},{r}");
            }
        }

        protected abstract bool IsCombinationSupport { get; }
        protected abstract bool IsNeedToCheckInventory { get; }
        protected abstract bool IsNeedToMatchTransport { get; }

        protected bool TestOrderIdIsRelated(Guid orderId)
        {
            return RelatedOrderIds.Any(id => id == orderId.ToString().ToLower());
        }

        public virtual void CheckInventory(bool automaticCheck = false)
        {
            if (IsNeedToCheckInventory)
            {
                // 需要检查库存的业务类型
                if (OrderState != OrderStatus.Submited)
                    throw new BusinessException(
                        message: $"Current order is {Enum.GetName(OrderState)} state, only [Created] state order could execute check inventory operation");

                OrderState = OrderStatus.CheckingStock;
                if (automaticCheck)
                {
                    AddLocalEvent(new CheckInventoryforOrderEvent { 
                        OrderId = Id,
                        BusinessType = BusinessType
                    });
                }
            }
        }

        public virtual void SetCheckedInventoryResult(IEnumerable<HeldStockResult> stockResult)
        {
            if (!IsNeedToCheckInventory)
                return;

            if (OrderState != OrderStatus.CheckingStock)
                throw new BusinessException(message: "Order state error");

            OrderState = OrderStatus.StockChecked;
        }

        public virtual void MatchTransportStrategy(bool automaticMatch = false)
        {
            if (!IsNeedToMatchTransport)
                return;

            if ((OrderState == OrderStatus.StockChecked && IsNeedToCheckInventory)
                || (OrderState == OrderStatus.Created && !IsNeedToCheckInventory))
            {
                OrderState = OrderStatus.MatchingTransportLine;
                if (automaticMatch)
                {
                    AddLocalEvent(new MatchTransportLineForOrdersEvent { 
                        OrderId = Id,
                        BusinessType = BusinessType
                    });
                }
                return;
            }

            throw new BusinessException(message: $"Current order didn't check inventory yet");
        }

        public virtual void SetMatchedResult(TransportStrategy strategy)
        {
            if (!IsNeedToMatchTransport)
                return;

            if (OrderState == OrderStatus.MatchingTransportLine)
            {
                OrderState = OrderStatus.TransportLineMatched;
                MatchedTransportStrategy = strategy;
            }
            else
                throw new BusinessException(message: $"Order state error");
        }

        public virtual void Cancel()
        {
            if ((int)OrderState > 5)
                throw new BusinessException(message: $"只能取消未开始分拣订单，当前订单状态为:[{Enum.GetName(OrderState)}]");

            //if (GetLocalEvents().Any(e => typeof(CancelOrderEvent) == e.EventOrder.GetType()))
            //    OrderState = OrderStatus.Cancelled;
            OrderState = OrderStatus.Cancelled;
            AddLocalEvent(new CancelOrderEvent { 
                OrderId = Id,
                BusinessType = BusinessType
            });
        }

        public virtual void Dispatching(bool automaticDispatch = false)
        {
            if (IsNeedToMatchTransport && OrderState != OrderStatus.TransportLineMatched)
                throw new BusinessException(message: $"Order state error which OrderNumber is {OrderNumber}");
            if (IsNeedToCheckInventory && OrderState != OrderStatus.StockChecked)
                throw new BusinessException(message: $"Order state error which OrderNumber is {OrderNumber}");

            if (automaticDispatch)
                AddLocalEvent(new DispatchOrdersEvent
                {
                    OrderId = Id,
                    BusinessType = BusinessType
                });
        }

        public virtual void Dispatched()
        {
            if (IsNeedToMatchTransport && OrderState != OrderStatus.TransportLineMatched)
                throw new BusinessException(message: $"Order state error which OrderNumber is {OrderNumber}");
            if (IsNeedToCheckInventory && OrderState != OrderStatus.StockChecked)
                throw new BusinessException(message: $"Order state error which OrderNumber is {OrderNumber}");

            OrderState = OrderStatus.Dispatched;
        }

        protected abstract void WithdrawDispatchCore();

        public virtual void WithdrawDispatch()
        {
            if (OrderState != OrderStatus.Dispatched)
                throw new BusinessException(message: $"Order state error");
            WithdrawDispatchCore();
        }

        public virtual void SuccessUndoDispatch()
        {
            if (OrderState != OrderStatus.Dispatched)
                throw new BusinessException(message: $"Order state error");

            if (IsNeedToCheckInventory && IsNeedToMatchTransport)
                OrderState = OrderStatus.TransportLineMatched;
            if (IsNeedToCheckInventory && !IsNeedToMatchTransport)
                OrderState = OrderStatus.StockChecked;
            if (!IsNeedToCheckInventory && IsNeedToMatchTransport)
                OrderState = OrderStatus.TransportLineMatched;
        }
    }
}
