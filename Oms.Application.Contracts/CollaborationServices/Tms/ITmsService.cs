using Oms.Application.Orders;
using Oms.Domain.Orders;

namespace Oms.Application.Contracts.CollaborationServices.Tms
{
    public interface ITmsService
    {
        Task<DataResult<IEnumerable<IssueToTmsResult>>> DispatchOrdersAsync(BusinessTypes businessType, IEnumerable<BusinessOrderDto> orders, IEnumerable<AddressDescription> addresses);
    }
}
