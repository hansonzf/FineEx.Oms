using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Quartz;
using System.Collections.ObjectModel;

namespace Oms.Application.Jobs
{
    public class FineExOmsJobManager : IJobManager
    {
        private readonly IScheduler scheduler;
        private readonly IJobDataMapperFactory jobDataMapperFactory;

        public FineExOmsJobManager(IScheduler scheduler, IJobDataMapperFactory jobDataMapperFactory)
        {
            this.scheduler = scheduler;
            this.jobDataMapperFactory = jobDataMapperFactory;
        }

        public async Task<bool> ExistJobAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            return await scheduler.CheckExists(jobKey);
        }

        private TriggerKey? SelectPrivateTriggerKey(IReadOnlyCollection<ITrigger> triggers)
        {
            if (triggers is null || !triggers.Any())
                return null;

            ITrigger privateTrigger = triggers.FirstOrDefault(t => t.Key.Group.StartsWith("private"));
            if (privateTrigger == null)
                return null;
            else
                return privateTrigger.Key;
        }

        public async Task<ProcessingJobDto?> ScheduleAsync(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing, double delayStart = 0)
        {
            if (!orders.Any()) return null;

            if (orders.Count() == 1 && orders.First().RelationType == RelationTypes.StandAlone)
                return await ScheduleAsync(orders.First(), processing, delayStart);

            var masterOrder = orders.SingleOrDefault(o => o.RelationType == RelationTypes.CombinedMaster);
            if (masterOrder == null) return null;
            var jobKey = JobHelper.GetJobKey(masterOrder.Id, processing);
            if (await scheduler.CheckExists(jobKey))
            {
                var triggers = await scheduler.GetTriggersOfJob(jobKey);
                var privateTrigger = SelectPrivateTriggerKey(triggers);
                return new ProcessingJobDto
                {
                    JobName = jobKey.Name,
                    GroupName = jobKey.Group,
                    TriggerName = privateTrigger?.Name
                };
            }

            var jobDetail = CreateJob(jobKey, orders, processing);
            var triggerList = CreateTriggers(masterOrder.Id, processing, delayStart);
            var defaultTrigger = SelectPrivateTriggerKey(triggerList);
            await scheduler.ScheduleJob(jobDetail, triggerList, true);

            return new ProcessingJobDto
            {
                JobName = jobKey.Name,
                GroupName = jobKey.Group,
                TriggerName = defaultTrigger.Name
            };
        }

        public async Task<ProcessingJobDto> ScheduleAsync(BusinessOrderDto order, ProcessingSteps processing, double delayStart = 0)
        {
            var jobKey = JobHelper.GetJobKey(order.Id, processing);
            if (await scheduler.CheckExists(jobKey))
            { 
                var triggers = await scheduler.GetTriggersOfJob(jobKey);
                var privateTrigger = SelectPrivateTriggerKey(triggers);
                return new ProcessingJobDto
                { 
                    JobName = jobKey.Name,
                    GroupName = jobKey.Group,
                    TriggerName = privateTrigger?.Name
                };
            }

            var jobDetail = CreateJob(jobKey, new BusinessOrderDto[1] { order }, processing);
            var triggerList = CreateTriggers(order.Id, processing, delayStart);
            var defaultTrigger = SelectPrivateTriggerKey(triggerList);
            await scheduler.ScheduleJob(jobDetail, triggerList, true);

            return new ProcessingJobDto 
            {
                JobName = jobKey.Name,
                GroupName = jobKey.Group,
                TriggerName = defaultTrigger.Name
            };
        }

        private IJobDetail CreateJob(JobKey jobKey, IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing)
        {
            Type jobType = processing switch
            {
                ProcessingSteps.B2bCheckoutInventory => typeof(CheckOutboundInventoryJob),
                ProcessingSteps.MatchTransport => typeof(MatchTransportLineJob),
                ProcessingSteps.Dispatching => typeof(DispatchingJob),
                _ => throw new ArgumentException()
            };

            JobDataMap jobDataMap = GrabJobDataMap(orders, processing);

            var jobDetail = JobBuilder.Create(jobType)
                .WithIdentity(jobKey)
                .RequestRecovery()
                .SetJobData(jobDataMap)
                .Build();

            return jobDetail;
        }


        private ITrigger CreateDefaultTrigger(Guid orderId, ProcessingSteps processing, double delay)
        {
            var defaultTriggerKey = JobHelper.GetPrivateTriggerKey(orderId, processing);

            ITrigger trigger;
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(defaultTriggerKey);

            if (delay == 0)
                trigger = triggerBuilder.StartNow().Build();
            else
                trigger = triggerBuilder.StartAt(new DateTimeOffset(DateTime.Now.AddMilliseconds(delay))).Build();

            return trigger;
        }

        /// <summary>
        /// create private trigger and public trigger
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="processing"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private ReadOnlyCollection<ITrigger> CreateTriggers(Guid orderId, ProcessingSteps processing, double delay)
        {
            List<ITrigger> triggers = new List<ITrigger>();

            if (processing == ProcessingSteps.Dispatching)
            {
                ITrigger publicTrigger = TriggerBuilder.Create()
                    .WithIdentity(JobHelper.GetPublicTriggerKey())
                    .WithCronSchedule("0 0 18 ? * * *")
                    .Build();
                triggers.Add(publicTrigger);
            }
            else
            {
                triggers.Add(CreateDefaultTrigger(orderId, processing, delay));
            }

            return triggers.AsReadOnly();
        }

        private JobDataMap GrabJobDataMap(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing)
        {
            JobDataMap map = new JobDataMap();
            var jobDataMapper = jobDataMapperFactory.CreateJobDataMapper(processing);
            var dataMap = jobDataMapper.GrabJobData(orders);
            foreach (var item in dataMap)
            {
                map.Add(item.Key, item.Value);
            }

            return map;
        }

        public async Task RunAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            var triggers = await scheduler.GetTriggersOfJob(jobKey);
            var triggerKey = SelectPrivateTriggerKey(triggers);

            if (triggerKey is null)
                return;

            Guid orderId = Guid.Parse(jobKey.Name);
            ProcessingSteps proc = Enum.Parse<ProcessingSteps>(jobKey.Group);
            ITrigger newTrigger = CreateDefaultTrigger(orderId, proc, 0);
            await scheduler.RescheduleJob(triggerKey, newTrigger);
        }

        public async Task<bool> DeleteJobAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            return await scheduler.DeleteJob(jobKey);
        }
    }
}
