using Microsoft.AspNetCore.Mvc;

namespace Eodg.MedicalTracker.Api
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; }
        public IActionResult ActionResult { get; set; }
    }
}