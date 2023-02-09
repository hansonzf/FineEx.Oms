using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace Oms.Domain.Orders.Specifications
{
    public class DefaultOrderListSpecification : Specification<BusinessOrder>
    {
        public override Expression<Func<BusinessOrder, bool>> ToExpression()
        {
            return o => o.Visible == true && (o.RelationType != RelationTypes.CombinedSlave);
        }
    }
}
