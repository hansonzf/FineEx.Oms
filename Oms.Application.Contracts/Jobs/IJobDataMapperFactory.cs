using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public interface IJobDataMapperFactory
    {
        IJobDataMapper CreateJobDataMapper(ProcessingSteps step);
    }
}
