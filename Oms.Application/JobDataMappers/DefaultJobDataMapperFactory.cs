using Oms.Domain.Processings;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class DefaultJobDataMapperFactory : IJobDataMapperFactory, ISingletonDependency
    {
        readonly DispatchJobDataMapper dispatchMapper;
        readonly MatchTransportLineJobDataMapper matchTransportLineMapper;
        readonly CheckInventoryOutboundJobDataMapper checkInventoryOutboundMapper;
        public DefaultJobDataMapperFactory(
            DispatchJobDataMapper dispatchMapper, 
            MatchTransportLineJobDataMapper matchTransportLineMapper, 
            CheckInventoryOutboundJobDataMapper checkInventoryOutboundMapper)
        {
            this.dispatchMapper = dispatchMapper;
            this.matchTransportLineMapper = matchTransportLineMapper;
            this.checkInventoryOutboundMapper = checkInventoryOutboundMapper;
        }

        public IJobDataMapper CreateJobDataMapper(ProcessingSteps step)
        {
            return step switch
            {
                ProcessingSteps.B2bCheckinInventory => new CheckInventoryInboundJobDataMapper(),
                ProcessingSteps.B2bCheckoutInventory => checkInventoryOutboundMapper,
                ProcessingSteps.MatchTransport => matchTransportLineMapper,
                ProcessingSteps.Dispatching => dispatchMapper,
                ProcessingSteps.None => throw new BusinessException(),
                _ => throw new ArgumentNullException()
            };
        }
    }
}
