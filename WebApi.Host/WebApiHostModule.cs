using InventoryCenter.Client;
using Microsoft.AspNetCore.Cors;
using Oms.Application;
using Oms.EntityframeworkCore;
using Oms.HttpApi;
using OrderCenter.Client;
using System.Configuration;
using System.Text.Json;
using ThreePL.Client;
using Tms.Client;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Wms.Client;

namespace WebApi.Host
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(OmsHttpApiModule),
        typeof(InventoryCenterClientModule),
        typeof(ThreePlClientModule),
        typeof(TmsClientModule),
        typeof(WmsClientModule),
        typeof(OrderCenterClientModule),
        typeof(OmsApplicationModule),
        typeof(OmsEntityframeworkCoreModule),
        typeof(InventoryCenterClientModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class WebApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            context.Services.AddControllers().AddJsonOptions(config =>
            {
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;//原样输出
                config.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
            });
            context.Services.AddSwaggerGen();
            context.Services.AddHttpClient();
            ConfigureCors(context, configuration);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FinexEx OMS API");
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();
            app.UseCors();
            app.UseConfiguredEndpoints();
            app.UseUnitOfWork();
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
