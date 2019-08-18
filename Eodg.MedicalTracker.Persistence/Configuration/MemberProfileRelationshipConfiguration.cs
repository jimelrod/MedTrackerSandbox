using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class MemberProfileRelationshipConfiguration : IEntityTypeConfiguration<MemberProfileRelationship>
    {
        public void Configure(EntityTypeBuilder<MemberProfileRelationship> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}