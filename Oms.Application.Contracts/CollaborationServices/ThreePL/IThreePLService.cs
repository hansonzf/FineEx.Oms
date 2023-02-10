using Oms.Domain.Orders;
using Oms.Domain.Shared;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    public interface IThreePLService
    {
        Task<List<CustomerCargoOwner>> GetCustomerCargoOwner(string tenantId);
        Task<AddressDescription?> GetWarehouseAddress(string tenantId, WarehouseDescription warehouse);
        Task<List<TransportResource>> GetCarrier(string tenantId);
        Task<List<TransportResource>> GetLogisticsCenter(string tenantId);
        Task<TransportResource> GetLogisticsCenter(string tenantId, string logisticKey);
        Task<List<CustomerDescription>> GetCustomer(string tenantId);
        Task<List<CargoOwnerDescription>> GetCargoOwner(string tenantId, string customerId);
        Task<List<CargoOwnerDescription>> GetCargoOwner(string tenantId);
        Task<List<WarehouseDescription>> GetWarehouse(string tenantId, int consignerId);
        Task<List<AddressDescription>> GetAddress(string tenantId);
        Task<List<AddressDescription>> GetAddressByCustomer(string tenantId, string customerId);
        Task<List<RouteScheme>> GetRouteScheme(string tenantId);
        Task<RouteScheme> GetRouteSchemeById(string tenantId, string id);
        Task<List<Region>> GetRegion(string tenantId);
        Task<List<RouteScheme>> GetRouteScheme(string tenantId, string customerId);
        Task<DataResult<List<TransportPlanRet>>> GetTransPlan(string tenantId, OrderDeliveryInfo orderInfo);
    }
}
