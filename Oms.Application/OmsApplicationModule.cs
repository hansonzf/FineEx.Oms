using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oms.Application.Contracts;
using Oms.Application.Jobs;
using Oms.Domain;
using Quartz;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.Matchers;
using Quartz.Simpl;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Oms.Application
{
    [DependsOn(
        typeof(OmsDomainModule),
        typeof(OmsApplicationContractsModule),
        typeof(AbpAutoMapperModule)
        )]
    public class OmsApplicationModule : AbpModule
    {
        private IScheduler _scheduler;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            string connectionString = configuration.GetConnectionString("Default");
            string jobsConnectionString = configuration.GetConnectionString("Quartz");

            context.Services.AddAutoMapperObjectMapper<OmsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options => {
                options.AddMaps<OmsApplicationModule>();
            });

            context.Services.AddQuartz(conf =>
            {
                conf.SchedulerId = "fineex-oms-scheduler";
                conf.SchedulerName = "fineex-oms-scheduler";
                conf.UseMicrosoftDependencyInjectionJobFactory();
                conf.UseJobFactory<OmsJobFactory>();
                conf.UsePersistentStore(storeConf =>
                {
                    storeConf.UseProperties = true;
                    storeConf.UseSqlServer(adoConf =>
                    {
                        adoConf.ConnectionString = jobsConnectionString;
                        adoConf.TablePrefix = "QRTZ_";
                        adoConf.UseDriverDelegate<SqlServerDelegate>();

                    });
                    storeConf.UseSerializer<JsonObjectSerializer>();
                });
            });
            context.Services.AddSingleton<IJobListener, FineExJobListener>();
            //context.Services.AddTransient<MatchTransportLineJob>();
            //context.Services.AddTransient<MatchTransportLineJobDataMapper>();
            //context.Services.AddTransient<DispatchJobDataMapper>();

            context.Services.AddSingleton(serviceProvider =>
            {
                return AsyncHelper.RunSync(() => serviceProvider.GetRequiredService<ISchedulerFactory>().GetScheduler());
            });
        }

        public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            _scheduler = context.ServiceProvider.GetRequiredService<IScheduler>();
            var jobListener = context.ServiceProvider.GetRequiredService<IJobListener>();
            _scheduler.ListenerManager.AddJobListener(jobListener, GroupMatcher<JobKey>.AnyGroup());
            await _scheduler.Start();
            await RegisterTaskbuilderWorker(_scheduler);
        }

        public override async Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
        {
            if (_scheduler.IsStarted)
                await _scheduler.Shutdown();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            AsyncHelper.RunSync(() => OnApplicationShutdownAsync(context));
        }

        public async Task RegisterTaskbuilderWorker(IScheduler scheduler)
        {
            if (await scheduler.CheckExists(OmsBuildTaskWorker.Key))
            {
                await scheduler.DeleteJob(OmsBuildTaskWorker.Key);
            }
            // Todo: Move this value into configuration file
            int buildTaskWorkInterval = 30;
            var job = JobBuilder.Create<OmsBuildTaskWorker>()
                .WithIdentity(OmsBuildTaskWorker.Key)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(JobHelper.TaskBuilderJobTriggerName, JobHelper.GlobalTriggerGroupName)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(buildTaskWorkInterval)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}