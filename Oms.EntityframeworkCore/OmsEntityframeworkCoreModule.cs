using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Oms.EntityframeworkCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreSqlServerModule)
        )]
    public class OmsEntityframeworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var conf = context.Services.GetConfiguration();
            string connectionString = conf.GetConnectionString("Default");

            context.Services.AddAbpDbContext<OmsDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
            //context.Services.AddDbContext<OmsDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);

            //});
        }
    }
}