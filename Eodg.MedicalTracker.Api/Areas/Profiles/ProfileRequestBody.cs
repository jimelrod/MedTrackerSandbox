using System.ComponentModel.DataAnnotations;

namespace Eodg.MedicalTracker.Api.Areas.Profiles
{
    public class ProfileRequestBody
    {
        [Required]
        public string DisplayName { get; set; }
        public string Notes { get; set; }
    }
}