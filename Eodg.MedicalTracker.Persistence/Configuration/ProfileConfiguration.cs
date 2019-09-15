using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(p => p.DisplayName)
                .IsRequired();

            builder
                .Property(p => p.Notes)
                .IsRequired(false);

            builder
                .ApplyActivableConfiguration();

            builder
                .ApplyTimestampableConfiguration();

            builder
                .HasMany(p => p.MemberProfileRelationships)
                .WithOne(mp => mp.Profile)
                .HasForeignKey(mp => mp.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(p => p.ProfileMedications)
                .WithOne(pm => pm.Profile)
                .HasForeignKey(pm => pm.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}