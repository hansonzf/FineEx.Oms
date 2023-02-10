﻿using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Quartz;
using System.Collections.ObjectModel;

namespace Oms.Application.Jobs
{
    public class FineExOmsJobManager : IJobManager
    {
        private readonly IScheduler scheduler;

        public FineExOmsJobManager(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        public async Task<bool> ExistJobAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            return await scheduler.CheckExists(jobKey);
        }

        public async Task<IEnumerable<ProcessingJobDto>> GetExecutingJobs()
        {
            var executionContext = await scheduler.GetCurrentlyExecutingJobs();
            return executionContext.Select(x => new ProcessingJobDto { 
                JobName = x.JobDetail.Key.Name,
                GroupName = x.JobDetail.Key.Group,
                TriggerName = x.Trigger.Key.Name,
                TriggerGroup = x.Trigger.Key.Group
            });
        }

        public async Task<bool> CancelJobAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            return await scheduler.DeleteJob(jobKey);
        }

        public async Task RunAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            var triggers = await scheduler.GetTriggersOfJob(jobKey);

            if (!triggers.Any())
                return;

            var triggerKey = triggers.First().Key;
            Guid orderId = Guid.Parse(jobKey.Name);
            ProcessingSteps proc = Enum.Parse<ProcessingSteps>(triggerKey.Group);
            ITrigger newTrigger = CreateDefaultTrigger(orderId, proc, 0).First();
            await scheduler.RescheduleJob(triggerKey, newTrigger);
        }

        public async Task<ProcessingJobDto?> ScheduleAsync(Guid orderId, BusinessTypes businessType, ProcessingSteps processing, Dictionary<string, string> parameters, double delayMilliseconds = 0)
        {
            if (orderId == Guid.Empty || businessType == BusinessTypes.None || processing == ProcessingSteps.None) 
                return null;

            var jobKey = JobHelper.GetJobKey(orderId, businessType);
            if (await scheduler.CheckExists(jobKey))
            {
                var triggers = await scheduler.GetTriggersOfJob(jobKey);
                var trigger = triggers.FirstOrDefault();
                return new ProcessingJobDto
                {
                    JobName = jobKey.Name,
                    GroupName = jobKey.Group,
                    TriggerName = trigger.Key.Name,
                    TriggerGroup = trigger.Key.Group
                };
            }

            var jobDetail = CreateJob(jobKey, parameters, processing);
            var triggerList = CreateDefaultTrigger(orderId, processing, delayMilliseconds);
            var defaultTrigger = triggerList.First();
            await scheduler.ScheduleJob(jobDetail, triggerList, true);

            return new ProcessingJobDto
            {
                JobName = jobKey.Name,
                GroupName = jobKey.Group,
                TriggerName = defaultTrigger.Key.Name,
                TriggerGroup = defaultTrigger.Key.Group
            };
        }

        //public async Task<ProcessingJobDto?> ScheduleAsync(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing, double delayMilliseconds = 0)
        //{
        //    if (!orders.Any()) return null;

        //    if (orders.Count() == 1 && orders.First().RelationType == RelationTypes.StandAlone)
        //        return await ScheduleAsync(orders.First(), processing, delayMilliseconds);

        //    var masterOrder = orders.SingleOrDefault(o => o.RelationType == RelationTypes.CombinedMaster);
        //    if (masterOrder == null)
        //    { 
        //        // 传进来的参数中全部都是独立的order
        //    }
        //    var jobKey = JobHelper.GetJobKey(masterOrder.Id, processing);
        //    if (await scheduler.CheckExists(jobKey))
        //    {
        //        var triggers = await scheduler.GetTriggersOfJob(jobKey);
        //        var privateTrigger = SelectPrivateTriggerKey(triggers);
        //        return new ProcessingJobDto
        //        {
        //            JobName = jobKey.Name,
        //            GroupName = jobKey.Group,
        //            TriggerName = privateTrigger?.Name
        //        };
        //    }

        //    var jobDetail = CreateJob(jobKey, orders, processing);
        //    var triggerList = CreateDefaultTrigger(masterOrder.Id, processing, delayMilliseconds);
        //    var defaultTrigger = SelectPrivateTriggerKey(triggerList);
        //    await scheduler.ScheduleJob(jobDetail, triggerList, true);

        //    return new ProcessingJobDto
        //    {
        //        JobName = jobKey.Name,
        //        GroupName = jobKey.Group,
        //        TriggerName = defaultTrigger.Name
        //    };
        //}

        //private async Task<ProcessingJobDto> ScheduleAsync(BusinessOrderDto order, ProcessingSteps processing, double delayMilliseconds = 0)
        //{
        //    var jobKey = JobHelper.GetJobKey(order.Id, processing);
        //    if (await scheduler.CheckExists(jobKey))
        //    { 
        //        var triggers = await scheduler.GetTriggersOfJob(jobKey);
        //        var privateTrigger = SelectPrivateTriggerKey(triggers);
        //        return new ProcessingJobDto
        //        { 
        //            JobName = jobKey.Name,
        //            GroupName = jobKey.Group,
        //            TriggerName = privateTrigger?.Name
        //        };
        //    }

        //    var jobDetail = CreateJob(jobKey, new BusinessOrderDto[1] { order }, processing);
        //    var triggerList = CreateDefaultTrigger(order.Id, processing, delayMilliseconds);
        //    var defaultTrigger = SelectPrivateTriggerKey(triggerList);
        //    await scheduler.ScheduleJob(jobDetail, triggerList, true);

        //    return new ProcessingJobDto 
        //    {
        //        JobName = jobKey.Name,
        //        GroupName = jobKey.Group,
        //        TriggerName = defaultTrigger.Name
        //    };
        //}

        private IJobDetail CreateJob(JobKey jobKey, Dictionary<string, string> jobParameters, ProcessingSteps processing)
        {
            Type jobType = processing switch
            {
                ProcessingSteps.B2bCheckoutInventory => typeof(CheckOutboundInventoryJob),
                ProcessingSteps.MatchTransport => typeof(MatchTransportLineJob),
                ProcessingSteps.Dispatching => typeof(DispatchingJob),
                _ => throw new ArgumentException()
            };

            JobDataMap jobDataMap = new JobDataMap();
            if (jobParameters.Any())
            {
                foreach (var item in jobParameters)
                {
                    jobDataMap.Add(item.Key, item.Value);
                }
            }

            var jobDetail = JobBuilder.Create(jobType)
                .WithIdentity(jobKey)
                .RequestRecovery()
                .SetJobData(jobDataMap)
                .Build();

            return jobDetail;
        }


        private ReadOnlyCollection<ITrigger> CreateDefaultTrigger(Guid orderId, ProcessingSteps processing, double delay)
        {
            var defaultTriggerKey = JobHelper.GetDefaultTriggerKey(orderId, processing);

            ITrigger trigger;
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(defaultTriggerKey);

            if (delay == 0)
                trigger = triggerBuilder.StartNow().Build();
            else
                trigger = triggerBuilder.StartAt(new DateTimeOffset(DateTime.Now.AddMilliseconds(delay))).Build();

            return new ReadOnlyCollection<ITrigger>(new ITrigger[1] { trigger });
        }

        //private JobDataMap GrabJobDataMap(IEnumerable<BusinessOrderDto> orders, ProcessingSteps processing)
        //{
        //    JobDataMap map = new JobDataMap();
        //    var jobDataMapper = jobDataMapperFactory.CreateJobDataMapper(processing);
        //    var dataMap = jobDataMapper.GrabJobData(orders);
        //    foreach (var item in dataMap)
        //    {
        //        map.Add(item.Key, item.Value);
        //    }

        //    return map;
        //}


    }
}
