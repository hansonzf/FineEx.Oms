using Oms.Application.Contracts;
using Oms.Domain.Orders;
using Volo.Abp.Application.Services;

namespace Oms.Application.Orders
{
    public interface IOrderOperationAppService : IApplicationService
    {
        Task<BusinessOrderDto?> GetOrderByIdAsync(long orderId, BusinessTypes businessType);
        Task<ServiceResult> CheckStockAsync(IEnumerable<long> orderIds, BusinessTypes businessType);
        Task<ServiceResult> MatchingTransportStrategyAsync(IEnumerable<long> orderIds, BusinessTypes businessType);
        Task<ServiceResult> SetMatchedTransportStrategyAsync(long orderId, int matchType, BusinessTypes businessType, string strategyName, string strategyMemo, IEnumerable<TransportResource> resources);
        Task<ServiceResult> DispatchOrdersAsync(string tenantId, IEnumerable<long> orderIds, BusinessTypes businessType);
        Task<ServiceResult> UndoDispatchOrdersAsync(string tenantId, IEnumerable<long> orderIds, BusinessTypes businessType);
        Task<ServiceResult> CancelOrderAsync(IEnumerable<long> orderIds, BusinessTypes businessType);
    }
}
