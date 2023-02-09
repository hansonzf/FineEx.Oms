using Oms.Application.Orders;

namespace Oms.Application.Jobs
{
    public interface IJobDataMapper
    {
        Dictionary<string, string> GrabJobData(IEnumerable<BusinessOrderDto> orders);
    }
}
