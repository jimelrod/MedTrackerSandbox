using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Authorization
{
    public class MemberSpecificAuthorizationHandler
        : AuthorizationHandler<MemberSpecificAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MemberSpecificAuthorizationRequirement requirement)
        {
            var isSuccessful = requirement.IsMet(context);

            if (isSuccessful)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
