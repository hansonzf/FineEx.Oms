using Oms.Domain.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Oms.Domain
{
    [DependsOn(
        typeof(OmsDomainSharedModule),
        typeof(AbpDddDomainModule)
        )]
    public class OmsDomainModule : AbpModule
    {

    }
}