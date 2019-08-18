using Eodg.MedicalTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eodg.MedicalTracker.Persistence
{
    public class MedicalTrackerDbContext : DbContext
    {
        public MedicalTrackerDbContext(DbContextOptions<MedicalTrackerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MemberProfileRelationship> MemberProfileRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}