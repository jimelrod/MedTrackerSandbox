using Eodg.MedicalTracker.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Eodg.MedicalTracker.Api.Areas.Healthcheck
{
    public class HealthCheckController : BaseController
    {
        public IActionResult Get()
        {
            return Ok(new { Status = "It's Good..." });
        }
    }
}