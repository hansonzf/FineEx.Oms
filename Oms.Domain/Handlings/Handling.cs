using Volo.Abp.Domain.Entities;

namespace Oms.Domain.Handlings
{
    public class Handling : BasicAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
        public DateTime ExecuteTime { get; set; }
        public string HandleBy { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
    }
}
