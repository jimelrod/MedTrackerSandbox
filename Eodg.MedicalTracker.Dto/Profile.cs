using System.Collections.Generic;

namespace Eodg.MedicalTracker.Dto
{
    public class Profile: IOwnableResource
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Member> Owners { get; set; }
    }
}
