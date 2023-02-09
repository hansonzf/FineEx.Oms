using Oms.Application.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public interface IJobManager : ITransientDependency
    {
        Task<ProcessingJobDto> ScheduleAsync(BusinessOrderDto order, ProcessingSteps processing, double delayStart = 0);
        Task<ProcessingJobDto?> ScheduleAsync(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing, double delayStart = 0);
        Task<bool> ExistJobAsync(string jobName, string jobGroup);
        Task<bool> DeleteJobAsync(string jobName, string jobGroup);
        Task RunAsync(string jobName, string jobGroup);
    }
}
