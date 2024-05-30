using _3_InspectionBackEnd_Infrastructure.DataSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_InspectionBackEnd_Infrastructure.Settings;
using _3_InspectionBackEnd_Infrastructure.Extensions;
using _2_InspectionBackEnd_Application.Interfaces;
using Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Quartz;
using _2_InspectionBackEnd_Application.Scheduler;

namespace _3_InspectionBackEnd_Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.LoadSettings<Infrastructure_Setting>(configuration);
            var settings = configuration.Get<Infrastructure_Setting>();
            services.AddDbContext(db =>
            {
                db.DbConnect<IInspection_Datasource, InspectionDatabaseContext>(configuration);

            });
            services.AddDbContext<InspectionDatabaseContext>(
                o => o.UseNpgsql(settings.ConnectionStrings.FirstOrDefault().ConStringValue));

            services.AddSingleton<IJwt, JwtTokenGenerator>();
            services.AddQuartz(o =>
            {
                o.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create("UpdateOlxTokpedData");
                o
                .AddJob<InsertUpdateOlxTokpedData>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInHours(168).RepeatForever()));

            });

            services.AddQuartzHostedService(o =>
            {
                o.WaitForJobsToComplete = true;
            });
            return services;
        }
        private static IServiceCollection LoadSettings<TSetting>(this IServiceCollection services, ConfigurationManager configuration)
            where TSetting : Infrastructure_Setting, new ()
        {
            var settingsDirectory = Path.Combine(AppContext.BaseDirectory, "Settings");
            var files = Directory.GetFiles(settingsDirectory, "*.json");
            foreach(var file in files)
            {
                configuration.AddJsonFile(file);
            }
            services.Configure<Infrastructure_Setting>(configuration);
            
            services.Configure<TSetting>(configuration);
            return services;
        }
        private static IServiceCollection AddDbContext(this IServiceCollection services, Action<IServiceCollection> dbContext)
        {
            dbContext(services);
            return services;
        }
    }
}
