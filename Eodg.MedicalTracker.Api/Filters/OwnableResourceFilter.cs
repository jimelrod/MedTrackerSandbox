using Eodg.MedicalTracker.Api.Authorization;
using Eodg.MedicalTracker.Services.Exceptions;
using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Filters
{
    public class OwnableResourceFilter : Attribute, IResourceFilter
    {
        private readonly string _routeKey;
        private readonly Type _resourceServicetype;

        public OwnableResourceFilter(Type resourceServicetype, string routeKey = "id")
        {
            _resourceServicetype = resourceServicetype;
            _routeKey = routeKey;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Resolve services...
            var authorizationService = (IAuthorizationService)context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService));
            var ownableResourceService = (IOwnableResourceService)context.HttpContext.RequestServices.GetService(_resourceServicetype);

            var id = int.Parse(context.RouteData.Values[_routeKey].ToString());

            // Get owners...
            IEnumerable<string> ownersFirebaseIds;

            try
            {
                ownersFirebaseIds =
                    ownableResourceService
                        .GetOwners(id)
                        .Select(member => member.FirebaseId);
            }
            catch (ResourceNotFoundException)
            {
                context.Result = new NotFoundResult();
                return;
            }

            // Authorize resource access...
            var authorizationTask = Task.Run(async () =>
            {
                var user = context.HttpContext.User;
                var requirement = new MemberSpecificAuthorizationRequirement(ownersFirebaseIds);

                return await authorizationService.AuthorizeAsync(user, null, requirement);
            });

            if (!authorizationTask.Result.Succeeded)
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}