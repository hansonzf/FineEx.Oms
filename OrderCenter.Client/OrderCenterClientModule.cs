using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts.CollaborationServices.OrderCenter;
using Volo.Abp.Modularity;

namespace OrderCenter.Client
{
    public class OrderCenterClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var conf = context.Services.GetConfiguration();
            string baseUrl = conf["ServiceDependency:OrderCenter:BaseUrl"];
            string appKey = conf["ServiceDependency:OrderCenter:AppKey"];
            string appSecret = conf["ServiceDependency:OrderCenter:AppSecret"];
            context.Services.AddSingleton<IOrderCenterService, OrderCenterService>(p =>
                new OrderCenterService(baseUrl, appKey, appSecret));
        }
    }
}