// using Eodg.MedicalTracker.Domain;
using AutoMapper;
using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Dto;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Exceptions;
using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    // TODO: We'll need some try/catch and custom exceptions...
    // TODO: Sub-TODO... Make sure all methods are doing what they can to minimize DB calls (use better helpers)
    // TODO: Look into logging all of the queries...
    public class ProfileService : ResourceService, IProfileService, IOwnableResourceService
    {
        public ProfileService(MedicalTrackerDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public Dto.Profile Get(int profileId)
        {
            Domain.Profile profile;

            try
            {
                profile = GetPopulatedProfiles().Single(p => p.Id == profileId);
            }
            catch (InvalidOperationException ex)
            {
                throw new ResourceNotFoundException($"Profile not found. Id: {profileId}", ex);
            }

            return Mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> GetAsync(int profileId)
        {
            var profile = await GetPopulatedProfiles().SingleOrDefaultAsync(p => p.Id == profileId);

            return Mapper.Map<Dto.Profile>(profile);
        }

        IOwnableResource IOwnableResourceService.Get(int profileId)
        {
            return Get(profileId);
        }

        async Task<IOwnableResource> IOwnableResourceService.GetAsync(int profileId)
        {
            return await GetAsync(profileId);
        }

        public IEnumerable<Dto.Profile> Get(
            string firebaseId,
            bool? isActive = true)
        {
            var profiles =
                DbContext
                    .MemberProfileRelationships
                    .Where(mp => mp.Member.FirebaseId == firebaseId)
                    .Select(mp => mp.Profile);

            if (isActive.HasValue)
            {
                profiles = profiles.Where(p => p.IsActive == isActive);
            }

            return profiles.Select(profile => Mapper.Map<Dto.Profile>(profile));
        }

        public async Task<IEnumerable<Dto.Profile>> GetAsync(
            string firebaseId,
            bool? isActive = true)
        {
            var domainProfiles =
                DbContext
                    .MemberProfileRelationships
                    .Where(mp => mp.Member.FirebaseId == firebaseId)
                    .Select(mp => mp.Profile);

            if (isActive.HasValue)
            {
                domainProfiles = domainProfiles.Where(p => p.IsActive == isActive);
            }

            var profileList = await domainProfiles.ToListAsync();

            return Mapper.Map<IEnumerable<Dto.Profile>>(profileList);
        }

        public Dto.Profile Add(
            string firebaseId,
            string displayName,
            string notes)
        {
            var profile = GenerateProfile(firebaseId, displayName, notes);

            DbContext.Add(profile);
            DbContext.SaveChanges();

            AddOwner(profile.Id, firebaseId);

            return Mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> AddAsync(
            string firebaseId,
            string displayName,
            string notes)
        {
            var profile = GenerateProfile(firebaseId, displayName, notes);

            DbContext.Add(profile);
            await DbContext.SaveChangesAsync();

            AddOwner(profile.Id, firebaseId);

            return Mapper.Map<Dto.Profile>(profile);
        }

        public Dto.Profile Update(
            string firebaseId,
            int profileId,
            string displayName,
            string notes)
        {
            var profile = GetPopulatedProfiles().SingleOrDefault(p => p.Id == profileId);

            profile.DisplayName = displayName;
            profile.Notes = notes;

            profile.AddTimestamp(firebaseId);

            DbContext.Update(profile);
            DbContext.SaveChanges();

            return Mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> UpdateAsync(
            string firebaseId,
            int profileId,
            string displayName,
            string notes)
        {
            var profile = await GetPopulatedProfiles().SingleOrDefaultAsync(p => p.Id == profileId);

            profile.DisplayName = displayName;
            profile.Notes = notes;

            profile.AddTimestamp(firebaseId);

            DbContext.Update(profile);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<Dto.Profile>(profile);
        }

        public Dto.Profile Activate(string firebaseId, int profileId)
        {
            return SetActivation(firebaseId, profileId, true);
        }

        public async Task<Dto.Profile> ActivateAsync(string firebaseId, int profileId)
        {
            return await SetActivationAsync(firebaseId, profileId, true);
        }

        public Dto.Profile Deactivate(string firebaseId, int profileId)
        {
            return SetActivation(firebaseId, profileId, false);
        }

        public async Task<Dto.Profile> DeactivateAsync(string firebaseId, int profileId)
        {
            return await SetActivationAsync(firebaseId, profileId, false);
        }

        public void Delete(int profileId)
        {
            var profile = DbContext.Profiles.SingleOrDefault(p => p.Id == profileId);

            DbContext.Remove(profile);
            DbContext.SaveChanges();
        }

        public async Task DeleteAsync(int profileId)
        {
            var profile = await DbContext.Profiles.SingleOrDefaultAsync(p => p.Id == profileId);

            DbContext.Remove(profile);
            await DbContext.SaveChangesAsync();
        }

        #region Profile Ownership

        public Dto.Profile AddOwner(int profileId, string firebaseId)
        {
            var member = DbContext.Members.Single(m => m.FirebaseId == firebaseId);

            return AddOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> AddOwnerAsync(int profileId, string firebaseId)
        {
            var member = await DbContext.Members.SingleAsync(m => m.FirebaseId == firebaseId);

            return await AddOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile AddOwner(int profileId, int memberId)
        {
            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = memberId,
                ProfileId = profileId
            };

            DbContext.Add(memberProfileRelationship);
            DbContext.SaveChanges();

            return Get(profileId);
        }

        public async Task<Dto.Profile> AddOwnerAsync(int profileId, int memberId)
        {
            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = memberId,
                ProfileId = profileId
            };

            DbContext.Add(memberProfileRelationship);
            await DbContext.SaveChangesAsync();

            return await GetAsync(profileId);
        }

        public Dto.Profile RemoveOwner(int profileId, string firebaseId)
        {
            var member = DbContext.Members.Single(m => m.FirebaseId == firebaseId);

            return RemoveOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, string firebaseId)
        {
            var member = await DbContext.Members.SingleAsync(m => m.FirebaseId == firebaseId);

            return await RemoveOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile RemoveOwner(int profileId, int memberId)
        {
            var memberProfileRelationship =
                DbContext
                    .MemberProfileRelationships
                    .Single(mp => mp.ProfileId == profileId && mp.MemberId == memberId);

            DbContext.Remove(memberProfileRelationship);
            DbContext.SaveChanges();

            return Get(profileId);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, int memberId)
        {
            var memberProfileRelationship =
                DbContext
                    .MemberProfileRelationships
                    .Single(mp => mp.ProfileId == profileId && mp.MemberId == memberId);

            DbContext.Remove(memberProfileRelationship);
            await DbContext.SaveChangesAsync();

            return await GetAsync(profileId);
        }

        #endregion

        #region Helper Methods

        private IIncludableQueryable<Domain.Profile, Domain.Member> GetPopulatedProfiles()
        {
            return
                DbContext
                    .Profiles
                    .Include(p => p.MemberProfileRelationships)
                    .ThenInclude(mp => mp.Member);
        }

        private Dto.Profile SetActivation(
            string firebaseId,
            int profileId,
            bool isActive)
        {
            var profile = GetPopulatedProfiles().SingleOrDefault(p => p.Id == profileId);

            profile.IsActive = isActive;
            profile.AddTimestamp(firebaseId);

            DbContext.Update(profile);
            DbContext.SaveChanges();

            return Mapper.Map<Dto.Profile>(profile);
        }

        private async Task<Dto.Profile> SetActivationAsync(
            string firebaseId,
            int profileId,
            bool isActive)
        {
            var profile = await GetPopulatedProfiles().SingleOrDefaultAsync(p => p.Id == profileId);

            profile.IsActive = isActive;
            profile.AddTimestamp(firebaseId);

            DbContext.Update(profile);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<Dto.Profile>(profile);
        }

        private static Domain.Profile GenerateProfile(
            string firebaseId,
            string displayName,
            string notes)
        {
            var profile = new Domain.Profile
            {
                DisplayName = displayName,
                Notes = notes,
                IsActive = true
            };

            profile.AddTimestamp(firebaseId, true);

            return profile;
        }

        #endregion
    }
}
