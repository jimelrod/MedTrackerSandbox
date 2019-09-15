using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class ProfileMedicationConfiguration : IEntityTypeConfiguration<ProfileMedication>
    {
        public void Configure(EntityTypeBuilder<ProfileMedication> builder)
        {
            builder
                .HasKey(pm => pm.Id);

            builder
                .Property(pm => pm.ProfileId)
                .IsRequired();

            builder
                .Property(pm => pm.MedicationId)
                .IsRequired();

            builder
                .Property(pm => pm.DoseMeasurementId)
                .IsRequired();

            builder
                .Property(pm => pm.Notes)
                .IsRequired(false);

            builder
                .Property(pm => pm.DoseQuantity)
                .IsRequired();

            builder
                .Property(pm => pm.DoseFrequencyInHours)
                .IsRequired(false);

            builder
                .ApplyActivableConfiguration();

            builder
                .ApplyTimestampableConfiguration();
        }
    }
}