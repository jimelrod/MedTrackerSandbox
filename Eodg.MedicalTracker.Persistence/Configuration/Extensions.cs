using Eodg.MedicalTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eodg.MedicalTracker.Persistence.Configuration
{
    public static class Extensions
    {
        public static void ApplyActivableConfiguration<T>(this EntityTypeBuilder<T> builder)
            where T : class, IActivable
        {
            builder
                .Property(e => e.IsActive)
                .IsRequired();
        }

        public static void ApplyTimestampableConfiguration<T>(this EntityTypeBuilder<T> builder)
            where T : class, ITimestampable
        {
            builder
                .Property(e => e.CreatedBy)
                .IsRequired();

            builder
                .Property(e => e.CreatedOn)
                .IsRequired();

            builder
                .Property(e => e.ModifiedBy)
                .IsRequired();

            builder
                .Property(e => e.ModifiedOn)
                .IsRequired();
        }
    }
}