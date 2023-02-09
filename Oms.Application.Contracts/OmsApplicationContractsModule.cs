using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Oms.Application.Contracts
{
    [DependsOn(typeof(AbpDddApplicationContractsModule))]
    public class OmsApplicationContractsModule : AbpModule
    {

    }
}