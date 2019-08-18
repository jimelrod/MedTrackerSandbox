using Microsoft.Extensions.DependencyInjection;

namespace Eodg.MedicalTracker.Api.Authorization
{
    public static class FirebaseAuthorizationExtensions
    {
        public static IServiceCollection AddFirebaseAuthorization(this IServiceCollection services, string policyName)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(policyName, policy =>
                {
                    policy.AddRequirements(new MemberSpecificAuthorizationRequirement());
                });
            });

            return services;
        }
    }
}