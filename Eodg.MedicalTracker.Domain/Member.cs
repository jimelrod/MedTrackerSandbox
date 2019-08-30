using Eodg.MedicalTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Eodg.MedicalTracker.Domain
{
    public partial class Member : IEntity, IActivable
    {
        public Member()
        {
            MemberProfileRelationships = new HashSet<MemberProfileRelationship>();
        }

        public int Id { get; set; }
        public string FirebaseId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public ICollection<MemberProfileRelationship> MemberProfileRelationships { get; set; }
    }
}