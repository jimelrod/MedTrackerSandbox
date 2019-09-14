using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Eodg.MedicalTracker.Common
{
    public static class Extensions
    {
        public static IServiceCollection AutoRegisterDependencies(this IServiceCollection services, params Type[] types)
        {
            var assembliesToScan = types.Select(type => Assembly.GetAssembly(type)).ToList();

            assembliesToScan.Add(Assembly.GetCallingAssembly());

            services
                .RegisterAssemblyPublicNonGenericClasses(assembliesToScan.ToArray())
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}
