using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eodg.MedicalTracker.Services
{
    public static class Extensions
    {
        public static void ResetContext(this DbContext dbContext)
        {
            dbContext
                .ChangeTracker
                .Entries()
                .Where(e => e.Entity != null)
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }

        public static void ResetContext<T>(this DbContext dbContext)
            where T : class
        {
            dbContext
                .ChangeTracker
                .Entries<T>()
                .Where(e => e.Entity != null)
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }
    }
}