using Eodg.MedicalTracker.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Authorization
{
    public class MemberSpecificAuthorizationHandler
        : AuthorizationHandler<MemberSpecificAuthorizationRequirement, IOwnableResource>
    {
        // protected override Task HandleRequirementAsync(
        //     AuthorizationHandlerContext context,
        //     MemberSpecificAuthorizationRequirement requirement,
        //     IOwnableResource resource)
        // {
        //     var isSuccessful =
        //         resource
        //             .Owners
        //             .Any(member =>
        //             {
        //                 return
        //                     context
        //                         .User
        //                         .HasClaim(ClaimTypes.NameIdentifier, member.FirebaseId);
        //             });

        //     if (isSuccessful)
        //     {
        //         context.Succeed(requirement);
        //     }

        //     return Task.CompletedTask;
        // }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberSpecificAuthorizationRequirement requirement, IOwnableResource resource)
        {
            throw new System.NotImplementedException();
        }
    }
}
