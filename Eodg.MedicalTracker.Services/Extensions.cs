using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Domain.Interfaces;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public static T SetActivation<T>(this IDataService<T> dataService, string firebaseId, int id, bool isActive)
            where T : class, IEntity, IActivable, ITimestampable
        {
            var entity = dataService.Get(id);

            entity.IsActive = isActive;
            entity.AddTimestamp(firebaseId);

            dataService.Update(entity);

            return entity;
        }

        public static async Task<T> SetActivationAsync<T>(this IDataService<T> dataService, string firebaseId, int id, bool isActive)
            where T : class, IEntity, IActivable, ITimestampable
        {
            var entity = await dataService.GetAsync(id);

            entity.IsActive = isActive;
            entity.AddTimestamp(firebaseId);

            await dataService.UpdateAsync(entity);

            return entity;
        }
    }
}