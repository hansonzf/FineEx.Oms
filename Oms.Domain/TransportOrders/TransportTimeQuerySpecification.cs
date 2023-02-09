using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace Oms.Domain.Orders.Specifications
{
    public class TransportTimeQuerySpecification : Specification<TransportOrder>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TimeOf { get; set; }

        public TransportTimeQuerySpecification(DateTime from, DateTime to, int timeOf)
        {
            From = from;
            To = to;
            TimeOf = timeOf;

            if (To < From)
                throw new ArgumentException("To time must large than From time");
        }

        public TransportTimeQuerySpecification(int timeOf, List<DateTime> timeRange)
        {
            TimeOf = timeOf;
            From = timeRange.FirstOrDefault();
            To = timeRange.LastOrDefault();
        }

        public override Expression<Func<TransportOrder, bool>> ToExpression()
        {
            return TimeOf switch
            {
                1 => o => o.ReceivedAt > From && o.ReceivedAt <= To,
                2 => o => o.FactPickupTime > From && o.FactPickupTime <= To,
                3 => o => o.FactCompleteTime > From && o.FactCompleteTime <= To,
                //4 => o => o.DeliveryInfo.ExpectingTime > From && o.DeliveryInfo.ExpectingTime <= To,
                //5 => o => o.DeliveryInfo.FactTime > From && o.DeliveryInfo.FactTime <= To,
                _ => throw new ArgumentException(nameof(TimeOf)),
            };
        }
    }
}
