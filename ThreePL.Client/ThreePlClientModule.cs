using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Volo.Abp.Modularity;

namespace ThreePL.Client
{
    [DependsOn(typeof(OmsApplicationContractsModule))]
    public class ThreePlClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IThreePLService, ThreePLService>();
        }
    }
}