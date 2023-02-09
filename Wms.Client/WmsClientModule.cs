using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.Wms;
using Volo.Abp.Modularity;

namespace Wms.Client
{
    [DependsOn(typeof(OmsApplicationContractsModule))]
    public class WmsClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var conf = context.Services.GetConfiguration();
            string baseUrl = conf["ServiceDependency:Wms:BaseUrl"];
            string appKey = conf["ServiceDependency:Wms:AppKey"];
            string appSecret = conf["ServiceDependency:Wms:AppSecret"];
            context.Services.AddSingleton<IWmsService, WmsService>(p =>
                new WmsService(baseUrl, appKey, appSecret));
        }
    }
}