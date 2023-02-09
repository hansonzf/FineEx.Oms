using Oms.Application.Contracts;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Oms.HttpApi
{
    [DependsOn(
        typeof(OmsApplicationContractsModule),
        typeof(AbpAutoMapperModule)
        )]
    public class OmsHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options => {
                options.AddMaps<OmsHttpApiModule>();
            });
        }
    }
}