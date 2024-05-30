using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using _2_InspectionBackEnd_Application.Behaviour;

namespace _2_InspectionBackEnd_Application
{
    public static class Startup
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validation_Behavior<,>));
            return services;

        }
    }
}
