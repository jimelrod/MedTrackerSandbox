using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
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
            var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
            var ownableResourceService = (IOwnableResourceService)context.HttpContext.RequestServices.GetService(_resourceServicetype);

            // Get resource...
            var id = int.Parse(context.RouteData.Values[_routeKey].ToString());            
            var resource = ownableResourceService.Get(id);

            if (resource == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            // Authorize resource access...
            var authorizationTask = Task.Run(async () => 
            {
                var user = context.HttpContext.User;
                var policyName = configuration["Authorization:PolicyName"];

                return await authorizationService.AuthorizeAsync(user, resource, policyName); 
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