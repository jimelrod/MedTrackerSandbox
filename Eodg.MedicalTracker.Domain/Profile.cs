using Eodg.MedicalTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Eodg.MedicalTracker.Domain
{
    public class Profile : IEntity, IActivable, ITimestampable
    {
        public Profile()
        {
            MemberProfileRelationships = new HashSet<MemberProfileRelationship>();
            ProfileMedications = new HashSet<ProfileMedication>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public ICollection<MemberProfileRelationship> MemberProfileRelationships { get; set; }
        public ICollection<ProfileMedication> ProfileMedications { get; set; }
    }
}