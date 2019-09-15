using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public class DoseMeasurementConfiguration : IEntityTypeConfiguration<DoseMeasurement>
    {
        public void Configure(EntityTypeBuilder<DoseMeasurement> builder)
        {
            builder
                .HasKey(dm => dm.Id);

            builder
                .HasAlternateKey(dm => dm.Name);

            builder
                .HasAlternateKey(dm => dm.Abbreviation);

            builder
                .Property(dm => dm.Name)
                .IsRequired();

            builder
                .Property(dm => dm.Abbreviation)
                .IsRequired();

            builder
                .HasMany(dm => dm.ProfileMedications)
                .WithOne(pm => pm.DoseMeasurement)
                .HasForeignKey(pm => pm.DoseMeasurementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}