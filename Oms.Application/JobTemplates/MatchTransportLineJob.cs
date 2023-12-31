﻿using Oms.Domain.Orders;
using Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Oms.Application.Jobs
{
    public class MatchTransportLineJob : IJob, ITransientDependency
    {
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uom;

        public MatchTransportLineJob(IOrderRepository repository, IUnitOfWorkManager uom)
        {
            this.repository = repository;
            this.uom = uom;
        }

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var unitOfWork = uom.Begin();
                var dataMap = context.MergedJobDataMap;
                string? id = dataMap.GetString(JobConstants.JobDataMapOrderIdKeyName);
                string? biz = dataMap.GetString(JobConstants.JobDataMapBusinessTypeKeyName);
                if (id == null || biz == null) return;
                long orderId = long.Parse(id);
                BusinessTypes businessType = biz switch
                {
                    "1" => BusinessTypes.OutboundWithTransport,
                    "2" => BusinessTypes.InboundWithTransport,
                    "3" => BusinessTypes.Transport,
                    _ => BusinessTypes.None
                };
                var order = await repository.GetOrderByIdAsync(orderId, businessType);
                if (order == null) return;
                order.MatchTransportStrategy(true);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(msg: ex.Message, refireImmediately: false, cause: ex);
            }
        }

        
    }
}
