using Eodg.MedicalTracker.Domain.Interfaces;

namespace Eodg.MedicalTracker.Domain
{
    public partial class MemberProfileRelationship : IEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProfileId { get; set; }

        public Member Member { get; set; }
        public Profile Profile { get; set; }
    }
}