using Microsoft.Extensions.Options;
using Oms.Application.Contracts;
using Oms.Application.Contracts.Processings;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Uow;

namespace Oms.Application.Processings
{
    public class ProcessingAppService : ApplicationService, IProcessingAppService
    {
        private readonly IProcessingRepository repository;

        public ProcessingAppService(IProcessingRepository repository)
        {
            this.repository = repository;
        }

        public async Task BuildingTask(Guid processingId)
        {
            var processing = await repository.GetAsync(processingId);
            if (processing is null)
                return;

            processing.BuildingTask();
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<DataResult<ProcessingDto>> CreateProcessingAsync(ProcessingDto dto)
        {
            var processing = ObjectMapper.Map<ProcessingDto, Processing>(dto);
            DataResult<ProcessingDto> result;
            try
            {
                await repository.InsertProcessingAsync(processing);
                result = new DataResult<ProcessingDto>
                {
                    Success = true,
                    Data = ObjectMapper.Map<Processing, ProcessingDto>(processing)
                };
            }
            catch (Exception ex)
            {
                result = new DataResult<ProcessingDto>
                {
                    Success = false,
                    Error = ex,
                    Message = ex.Message
                };
            }

            return result;
        }

        public async Task<ServiceResult> ExecuteTask(Guid orderId, ProcessingSteps proc)
        {
            var processing = await repository.GetByOrderIdAsync(orderId);
            if (processing is null)
                return new ServiceResult
                { 
                    Success = false,
                    Message = $"Task not found"
                };

            try
            {
                processing.StartRun(proc);
                return new ServiceResult
                {
                    Success = true
                };
            }
            catch (BusinessException ex)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
