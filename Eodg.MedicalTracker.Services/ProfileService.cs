using AutoMapper;
using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Data.Interfaces;
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
    public class ProfileService : IProfileService, IOwnableResourceService
    {
        private readonly IDataService<Domain.Profile> _profileDataService;
        private readonly IDataService<Domain.MemberProfileRelationship> _memberProfileRelationshipDataService;
        private readonly MedicalTrackerDbContext _dbContext;
        private readonly IMapper _mapper;
        private IDataService<Domain.Member> _memberDataService;

        public ProfileService(
            IDataService<Domain.Profile> profileDataService,
            IDataService<Domain.MemberProfileRelationship> memberProfileRelationshipDataService,
            IDataService<Domain.Member> memberDataService,
            MedicalTrackerDbContext dbContext,
            IMapper mapper)
        {
            _profileDataService = profileDataService;
            _memberProfileRelationshipDataService = memberProfileRelationshipDataService;
            _dbContext = dbContext;
            _mapper = mapper;
            _memberDataService = memberDataService;
        }

        public Dto.Profile Get(int profileId)
        {
            var profile = _profileDataService.GetSingle(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> GetAsync(int profileId)
        {
            var profile = await _profileDataService.GetSingleAsync(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public IEnumerable<Dto.Profile> Get(
            string firebaseId,
            bool? isActive = true)
        {
            // var profiles =
            //     DbContext
            //         .MemberProfileRelationships
            //         .Where(mp => mp.Member.FirebaseId == firebaseId)
            //         .Select(mp => mp.Profile);

            // var profiles = _memberProfileRelationshipDataService.Get(mp => mp.Member.FirebaseId == firebaseId).Select(mp => mp.Profile);

            // if (isActive.HasValue)
            // {
            //     profiles = profiles.Where(p => p.IsActive == isActive);
            // }

            // return profiles.Select(profile => _mapper.Map<Dto.Profile>(profile));

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Dto.Profile>> GetAsync(
            string firebaseId,
            bool? isActive = true)
        {
            // var domainProfiles =
            //     _dbContext
            //         .MemberProfileRelationships
            //         .Where(mp => mp.Member.FirebaseId == firebaseId)
            //         .Select(mp => mp.Profile);

            // if (isActive.HasValue)
            // {
            //     domainProfiles = domainProfiles.Where(p => p.IsActive == isActive);
            // }

            // var profileList = await domainProfiles.ToListAsync();

            // return _mapper.Map<IEnumerable<Dto.Profile>>(profileList);

            throw new NotImplementedException();
        }

        public IEnumerable<Dto.Member> GetOwners(int id)
        {
            // Domain.Profile profile;

            // try
            // {
            //     profile =
            //         _dbContext
            //             .Profiles
            //             .Include(p => p.MemberProfileRelationships)
            //                 .ThenInclude(mp => mp.Member)
            //             .Single(p => p.Id == id);
            // }
            // catch (InvalidOperationException ex)
            // {
            //     var message = $"{typeof(Dto.Member)} not found. Id: {id}. See InnerException for details...";

            //     throw new ResourceNotFoundException(message, ex);
            // }

            // return profile.MemberProfileRelationships.Select(mp => _mapper.Map<Dto.Member>(mp.Member));

            throw new NotImplementedException();
        }

        public Dto.Profile Add(
            string firebaseId,
            string displayName,
            string notes)
        {
            var profile = GenerateProfile(firebaseId, displayName, notes);

            _profileDataService.Add(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> AddAsync(
            string firebaseId,
            string displayName,
            string notes)
        {
            var profile = GenerateProfile(firebaseId, displayName, notes);

            await _profileDataService.AddAsync(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public Dto.Profile Update(
            string firebaseId,
            int profileId,
            string displayName,
            string notes)
        {
            var profile = _profileDataService.GetSingle(profileId);

            profile.DisplayName = displayName;
            profile.Notes = notes;

            profile.AddTimestamp(firebaseId);

            _profileDataService.Add(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> UpdateAsync(
            string firebaseId,
            int profileId,
            string displayName,
            string notes)
        {
            var profile = await _profileDataService.GetSingleAsync(profileId);

            profile.DisplayName = displayName;
            profile.Notes = notes;

            profile.AddTimestamp(firebaseId);

            await _profileDataService.AddAsync(profile);

            return _mapper.Map<Dto.Profile>(profile);
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
            var profile = _dbContext.Profiles.SingleOrDefault(p => p.Id == profileId);

            _dbContext.Remove(profile);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(int profileId)
        {
            var profile = await _dbContext.Profiles.SingleOrDefaultAsync(p => p.Id == profileId);

            _dbContext.Remove(profile);
            await _dbContext.SaveChangesAsync();
        }

        #region Profile Ownership

        public Dto.Profile AddOwner(int profileId, string firebaseId)
        {
            var member = _memberDataService.GetSingle(m => m.FirebaseId == firebaseId);

            return AddOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> AddOwnerAsync(int profileId, string firebaseId)
        {
            var member = await _memberDataService.GetSingleAsync(m => m.FirebaseId == firebaseId);

            return await AddOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile AddOwner(int profileId, int memberId)
        {
            var profile = _profileDataService.GetSingle(profileId);

            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = memberId,
                ProfileId = profileId
            };

            profile.MemberProfileRelationships.Add(memberProfileRelationship);

            _profileDataService.Update(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> AddOwnerAsync(int profileId, int memberId)
        {
            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = memberId,
                ProfileId = profileId
            };

            _dbContext.Add(memberProfileRelationship);
            await _dbContext.SaveChangesAsync();

            return await GetAsync(profileId);
        }

        public Dto.Profile RemoveOwner(int profileId, string firebaseId)
        {
            var member = _memberDataService.GetSingle(m => m.FirebaseId == firebaseId);

            return RemoveOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, string firebaseId)
        {
            var member = await _memberDataService.GetSingleAsync(m => m.FirebaseId == firebaseId);

            return await RemoveOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile RemoveOwner(int profileId, int memberId)
        {
            var memberProfileRelationship =
                _dbContext
                    .MemberProfileRelationships
                    .Single(mp => mp.ProfileId == profileId && mp.MemberId == memberId);

            _dbContext.Remove(memberProfileRelationship);
            _dbContext.SaveChanges();

            return Get(profileId);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, int memberId)
        {
            var memberProfileRelationship =
                _dbContext
                    .MemberProfileRelationships
                    .Single(mp => mp.ProfileId == profileId && mp.MemberId == memberId);

            _dbContext.Remove(memberProfileRelationship);
            await _dbContext.SaveChangesAsync();

            return await GetAsync(profileId);
        }

        #endregion

        #region Helper Methods

        // TODO: This should either be removed or not used as much...
        private IIncludableQueryable<Domain.Profile, Domain.Member> GetPopulatedProfiles()
        {
            return
                _dbContext
                    .Profiles
                    .Include(p => p.MemberProfileRelationships)
                    .ThenInclude(mp => mp.Member);

            // return _dbContext.Profiles.Include(p => p.MemberProfileRelationships).ThenInclude()
        }

        private Dto.Profile SetActivation(
            string firebaseId,
            int profileId,
            bool isActive)
        {
            var profile = _profileDataService.GetSingle(profileId);

            profile.IsActive = isActive;
            profile.AddTimestamp(firebaseId);

            _profileDataService.Update(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        private async Task<Dto.Profile> SetActivationAsync(
            string firebaseId,
            int profileId,
            bool isActive)
        {
            var profile = await _profileDataService.GetSingleAsync(profileId);

            profile.IsActive = isActive;
            profile.AddTimestamp(firebaseId);

            await _profileDataService.UpdateAsync(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        private Domain.Profile GenerateProfile(
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

            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = _memberDataService.GetSingle(m => m.FirebaseId == firebaseId).Id
            };

            profile.MemberProfileRelationships.Add(memberProfileRelationship);

            return profile;
        }

        #endregion
    }
}
