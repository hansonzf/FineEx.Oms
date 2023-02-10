using Volo.Abp.Domain.Repositories;

namespace Oms.Domain.Orders
{
    public interface IOrderRepository : IRepository
    {
        Task<IEnumerable<BusinessOrder>> GetOrdersByBusinessType(IEnumerable<long> ids, BusinessTypes businessType);
        Task<IEnumerable<BusinessOrder>> GetWithCombinedOrdersAsync<TOrder>(Guid orderId)
            where TOrder : BusinessOrder;
        Task<BusinessOrder?> GetOrderByIdAsync(long id, BusinessTypes businessType);
        Task<TOrder?> GetAsync<TOrder>(Guid orderId)
            where TOrder : BusinessOrder;
        Task<TOrder?> GetAsync<TOrder>(string orderNumber)
            where TOrder : BusinessOrder;
        Task<TOrder?> GetAsync<TOrder>(long orderId)
            where TOrder : BusinessOrder;
        Task<Guid?> GetOrderUuidByOrderIdAsync(long orderId, BusinessTypes businessType);
        Task<long?> GetOrderIdByOrderUuidAsync(Guid orderUuid, BusinessTypes businessType);
        Task<TOrder> InsertOrderAsync<TOrder>(TOrder order)
            where TOrder : BusinessOrder;
        Task<bool> UpdateOrderAsync<TOrder>(TOrder order)
            where TOrder : BusinessOrder;
        Task<string> GetRelatedOrderIds(Guid masterOrderId, BusinessTypes businessType);
        Task<string> GetOrderTenantIdAsync(Guid orderId, BusinessTypes businessType);



        //Task<bool> ScheduledJobByOrder<TOrder>(Guid orderId, ProcessingSteps step)
        //    where TOrder : BusinessOrder;
        //Task<bool> ScheduledJobByOrder(Guid orderId, BusinessTypes businessType, ProcessingSteps step);
        //Task ReceivedTmsAcknowledgement(Guid orderId, BusinessTypes businessType, string transOrderNumber);
        //Task ReceivedWmsAcknowledgement(Guid orderId, BusinessTypes businessType, string wmsOrderNumber);
    }
}
