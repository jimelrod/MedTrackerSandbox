using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .HasAlternateKey(m => m.FirebaseId);

            builder
                .HasIndex(m => m.Email)
                .IsUnique();

            builder
                .Property(m => m.FirebaseId)
                .IsRequired();

            builder
                .Property(m => m.Email)
                .IsRequired();

            builder
                .Property(m => m.DisplayName)
                .IsRequired(false);

            builder
                .ApplyActivableConfiguration();

            builder
                .HasMany(m => m.MemberProfileRelationships)
                .WithOne(mp => mp.Member)
                .HasForeignKey(mp => mp.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}