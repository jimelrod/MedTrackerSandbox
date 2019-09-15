using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .HasAlternateKey(m => m.Name);

            builder
                .Property(m => m.Name)
                .IsRequired();

            builder
                .Property(m => m.Description)
                .IsRequired(false);

            builder
                .HasMany(m => m.ProfileMedications)
                .WithOne(pm => pm.Medication)
                .HasForeignKey(pm => pm.MedicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}