using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public interface IJobManager : ITransientDependency
    {
        //Task<ProcessingJobDto> ScheduleAsync(BusinessOrderDto order, ProcessingSteps processing, double delayMilliseconds = 0);
        Task<bool> ExistJobAsync(string jobName, string jobGroup);
        Task<IEnumerable<ProcessingJobDto>> GetExecutingJobs();
        Task<bool> CancelJobAsync(string jobName, string jobGroup);
        Task RunAsync(string jobName, string jobGroup);
        Task<ProcessingJobDto?> ScheduleAsync(Guid orderId, BusinessTypes businessType, ProcessingSteps processing, Dictionary<string, string> parameters, double delayMilliseconds = 0);
        //Task<ProcessingJobDto?> ScheduleAsync(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing, double delayMilliseconds = 0);
    }
}
