using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.Application.Dtos;

namespace Oms.Application.Contracts.Processings
{
    public class ProcessingDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public BusinessTypes BusinessType { get; set; }
        public ProcessingSteps Steps { get; set; }
        public int Processed { get; set; }
        public long SerialNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProcessing { get; set; }
        public bool IsCompleteAllSteps { get; set; }
        public string? JobName { get; set; }
        public string? TriggerName { get; set; }
        public string? GroupName { get; set; }
    }
}
