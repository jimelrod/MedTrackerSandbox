using Eodg.MedicalTracker.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Eodg.MedicalTracker.Api.Areas.Healthcheck
{
    public class HealthCheckSecured : SecuredController
    {
        public HealthCheckSecured()
        {
        }

        public IActionResult Get()
        {
            return Ok("User is logged on.");
        }
    }
}