using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.Tms;
using Volo.Abp.Modularity;

namespace Tms.Client
{
    [DependsOn(typeof(OmsApplicationContractsModule))]
    public class TmsClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var conf = context.Services.GetConfiguration();
            string baseUrl = conf["ServiceDependency:Tms:BaseUrl"];
            string appKey = conf["ServiceDependency:Tms:AppKey"];
            string appSecret = conf["ServiceDependency:Tms:AppSecret"];
            context.Services.AddSingleton<ITmsService, TmsService>(p =>
                new TmsService(baseUrl, appKey, appSecret));
        }
    }
}