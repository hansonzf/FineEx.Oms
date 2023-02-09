using Oms.Domain.Processings;
using Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Oms.Application
{
    public class OmsBuildTaskWorker : IJob, ITransientDependency, IUnitOfWorkEnabled
    {
        readonly IProcessingRepository repository;
        readonly IUnitOfWorkManager unitOfWorkManager;

        public static readonly JobKey Key = new("OmsTaskBuilder", "global");

        public OmsBuildTaskWorker(IProcessingRepository repository, IUnitOfWorkManager unitOfWorkManager)
        {
            this.repository = repository;
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var processings = await repository.GetWaitforBuildProcessing();
                // Todo: Move this value into configuration file
                int buildTaskWorkInterval = 30;
                double delayTime = 1000 * buildTaskWorkInterval / (processings.Count() + 1);
                if (processings.Any())
                {
                    foreach (var p in processings)
                    {
                        p.BuildingTask();
                    }

                    await unitOfWorkManager.Current.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(msg: ex.Message, refireImmediately: false, cause: ex);
            }
        }
    }
}
