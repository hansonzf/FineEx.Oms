using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.Inventory;
using Volo.Abp.Modularity;

namespace InventoryCenter.Client
{
    [DependsOn(typeof(OmsApplicationContractsModule))]
    public class InventoryCenterClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var conf = context.Services.GetConfiguration();
            string baseUrl = conf["ServiceDependency:Inventory:BaseUrl"];
            string appKey = conf["ServiceDependency:Inventory:AppKey"];
            string appSecret = conf["ServiceDependency:Inventory:AppSecret"];
            context.Services.AddSingleton<IInventoryService, InventoryService>(p =>
                new InventoryService(baseUrl, appKey, appSecret));
        }
    }
}