using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data
{
    public class ProfileDataService : DataService<Profile>, IProfileDataService
    {
        public ProfileDataService(MedicalTrackerDbContext dbContext)
            : base(dbContext)
        {
        }

        Profile IProfileDataService.Get(int id)
        {
            Profile profile;

            try
            {
                profile = GetLoadedProfiles().Single(p => p.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"Profile not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return profile;
        }

        async Task<Profile> IProfileDataService.GetAsync(int id)
        {
            Profile profile;

            try
            {
                profile = await GetLoadedProfiles().SingleAsync(p => p.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"Profile not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return profile;
        }

        IEnumerable<Profile> IProfileDataService.Get(Expression<Func<Profile, bool>> predicate)
        {
            IEnumerable<Profile> profiles;

            try
            {
                profiles = GetLoadedProfiles().Where(predicate).ToList();
            }
            catch (InvalidOperationException ex)
            {
                var message = $"This should never happen...";

                throw new ResourceNotFoundException(message, ex);
            }

            return profiles;
        }

        async Task<IEnumerable<Profile>> IProfileDataService.GetAsync(Expression<Func<Profile, bool>> predicate)
        {
            IEnumerable<Profile> profiles;

            try
            {
                profiles = await GetLoadedProfiles().Where(predicate).ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                var message = $"This should never happen...";

                throw new ResourceNotFoundException(message, ex);
            }

            return profiles;
        }

        public IEnumerable<Profile> Get(string firebaseId)
        {
            return Get(p => p.MemberProfileRelationships.Any(mp => mp.Member.FirebaseId == firebaseId));
        }

        public async Task<IEnumerable<Profile>> GetAsync(string firebaseId)
        {
            return await GetAsync(p => p.MemberProfileRelationships.Any(mp => mp.Member.FirebaseId == firebaseId));
        }

        private IQueryable<Profile> GetLoadedProfiles()
        {
            var profiles =
                DbContext
                    .Profiles
                    .Include(p => p.MemberProfileRelationships)
                        .ThenInclude(mp => mp.Member);

            return profiles;
        }
    }
}