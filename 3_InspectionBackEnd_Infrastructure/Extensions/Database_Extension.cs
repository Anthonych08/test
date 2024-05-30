using _3_InspectionBackEnd_Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.Extensions
{
    public static class Database_Extension
    {
        public static IServiceCollection DbConnect<ITContext, TContext>(this IServiceCollection services, ConfigurationManager configuration)
            where TContext : DbContext, ITContext where ITContext : class
        {
            var settings = configuration.Get<Infrastructure_Setting>();
            var db = settings!.ConnectionStrings.Find(w => w.ConStringName.ToLower().Trim() == "BookingMRAppDb".ToLower());
            var constring = "";
            constring = $"{db.ConStringValue}";
            services.AddDbContext<TContext>(options => { options.UseNpgsql(constring); });
            services.AddScoped<ITContext>(provider => provider.GetService<TContext>()!);
            return services;
        }
    }
}
