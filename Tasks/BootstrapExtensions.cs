using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Console;
using Tasks.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Tasks
{

    public static class BootstrapExtensions
    {
        public static IServiceCollection AddHangfireWorker(this IServiceCollection services, IConfiguration configuration)
        {
            AddHangfireBasics(services, configuration);
            services.AddHangfireServer();


            return services;
        }

        private static void AddHangfireBasics(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<EchoTask>();
            services.AddScoped<FailureTask>();
            services.AddScoped<RunMeEvery1Min>();
            services.AddScoped<TestDIJob>();
            services.AddTransient<AService>();

            services.AddHangfire(c => c
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseConsole()
                            .UseSqlServerStorage(configuration["HangfireConnection"], new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }));
        }

        public static IServiceCollection AddHangfireScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            AddHangfireBasics(services, configuration);

            return services;
        }

        public static IHost SetupHangfireDIWithServiceProviderForWorker(this IHost host)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(host.Services));
            return host;
        }

        public static IApplicationBuilder SetupHangfireDIWithServiceProvider(this IApplicationBuilder app)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(app.ApplicationServices));
            return app;
        }

        public static IApplicationBuilder ScheduleRecurringJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<RunMeEvery1Min>(nameof(RunMeEvery1Min), i => i.Yay(default), Cron.Minutely());
            RecurringJob.AddOrUpdate<RunMeEvery1Min>(nameof(RunMeEvery1Min) + "_HOURLY", i => i.Yay(default), Cron.Hourly());
            RecurringJob.AddOrUpdate<RunMeEvery1Min>(nameof(RunMeEvery1Min) + "_DAILY", i => i.Yay(default), Cron.Daily(5), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<RunMeEvery1Min>(nameof(RunMeEvery1Min) + "_MONHLY", i => i.Yay(default), Cron.Monthly());
            RecurringJob.AddOrUpdate<RunMeEvery1Min>(nameof(RunMeEvery1Min) + "_YEARLY", i => i.Yay(default), Cron.Yearly());
            return app;
        }
    }
}
