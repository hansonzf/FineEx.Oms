using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class OmsJobFactory : IJobFactory, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public OmsJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJobDetail jobDetail = bundle.JobDetail;
            Type jobType = jobDetail.JobType;
            try
            {
                return (IJob)_serviceProvider.GetRequiredService(jobType);
            }
            catch (Exception e)
            {
                SchedulerException se = new SchedulerException($"Problem instantiating class '{jobDetail.JobType.FullName}: {e.Message}'", e);
                throw se;
            }
        }

        public void ReturnJob(IJob job)
        {
            return;
        }
    }
}
