using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.Application.Services;

namespace Oms.Application.Contracts.Processings
{
    public interface IProcessingAppService : IApplicationService
    {
        Task ExecutedCheckStockAsync(Guid orderId, BusinessTypes businessType);
        Task<DataResult<ProcessingDto>> CreateProcessingAsync(ProcessingDto dto);
        Task BuildingTask(Guid processingId);
        Task<ServiceResult> ExecuteTask(Guid orderId, ProcessingSteps proc);
    }
}
