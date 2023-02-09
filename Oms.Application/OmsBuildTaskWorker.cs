using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Oms.Application
{
    public class OmsBuildTaskWorker : IJob, ITransientDependency, IUnitOfWorkEnabled
    {
        readonly IProcessingRepository repository;
        readonly IOrderRepository orderRepository;
        readonly IUnitOfWorkManager unitOfWorkManager;

        public static readonly JobKey Key = new("OmsTaskBuilder", "global");

        public OmsBuildTaskWorker(IProcessingRepository repository, IOrderRepository orderRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this.repository = repository;
            this.orderRepository = orderRepository;
            this.unitOfWorkManager = unitOfWorkManager;
        }

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var processings = await repository.GetWaitforBuildProcessing();
                double delayTime = 1 * 60 * 1000 / (processings.Count() + 1);
                if (processings.Any())
                {
                    foreach (var p in processings)
                    {
                        p.BuildingTask();
                        //var currentStep = p.GetCurrentStep();
                        //if (await orderRepository.ScheduledJobByOrder(p.OrderId, p.BusinessType, currentStep))
                        //    p.BuildingTask();
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
