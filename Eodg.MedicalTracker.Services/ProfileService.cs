using AutoMapper;
using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    // TODO: Look into logging all of the queries...
    public class ProfileService : IProfileService, IOwnableResourceService
    {
        private readonly IProfileDataService _profileDataService;
        private readonly IMemberProfileRelationshipDataService _memberProfileRelationshipDataService;
        private readonly IMapper _mapper;
        private readonly IMemberDataService _memberDataService;

        public ProfileService(
            IProfileDataService profileDataService,
            IMemberProfileRelationshipDataService memberProfileRelationshipDataService,
            IMemberDataService memberDataService,
            IMapper mapper)
        {
            _profileDataService = profileDataService;
            _memberProfileRelationshipDataService = memberProfileRelationshipDataService;
            _mapper = mapper;
            _memberDataService = memberDataService;
        }

        public Dto.Profile Get(int profileId)
        {
            var profile = _profileDataService.Get(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> GetAsync(int profileId)
        {
            var profile = await _profileDataService.GetAsync(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public IEnumerable<Dto.Profile> Get(
            string firebaseId,
            bool? isActive = true)
        {
            var profiles = _profileDataService.Get(firebaseId);

            if (isActive.HasValue)
            {
                profiles = profiles.Where(p => p.IsActive);
            }

            return profiles.Select(profile => _mapper.Map<Dto.Profile>(profile));
        }

        public async Task<IEnumerable<Dto.Profile>> GetAsync(
            string firebaseId,
            bool? isActive = true)
        {
            var profiles = await _profileDataService.GetAsync(firebaseId);

            if (isActive.HasValue)
            {
                profiles = profiles.Where(p => p.IsActive);
            }

            return profiles.Select(profile => _mapper.Map<Dto.Profile>(profile));
        }

        public IEnumerable<Dto.Member> GetOwners(int id)
        {
            var profile = _profileDataService.Get(id);

            var owners = profile.MemberProfileRelationships.Select(mp => mp.Member);

            return _mapper.Map<IEnumerable<Dto.Member>>(owners);
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
            var profile = _profileDataService.Get(profileId);

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
            var profile = await _profileDataService.GetAsync(profileId);

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
            var profile = _profileDataService.Get(profileId);

            _profileDataService.Delete(profile);
        }

        public async Task DeleteAsync(int profileId)
        {
            var profile = await _profileDataService.GetAsync(profileId);

            await _profileDataService.DeleteAsync(profile);
        }

        #region Profile Ownership

        public Dto.Profile AddOwner(int profileId, string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            return AddOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> AddOwnerAsync(int profileId, string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            return await AddOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile AddOwner(int profileId, int memberId)
        {
            var profile = _profileDataService.Get(profileId);

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
            var profile = await _profileDataService.GetAsync(profileId);

            var memberProfileRelationship = new MemberProfileRelationship
            {
                MemberId = memberId,
                ProfileId = profileId
            };

            profile.MemberProfileRelationships.Add(memberProfileRelationship);

            await _profileDataService.UpdateAsync(profile);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public Dto.Profile RemoveOwner(int profileId, string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            return RemoveOwner(profileId, member.Id);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            return await RemoveOwnerAsync(profileId, member.Id);
        }

        public Dto.Profile RemoveOwner(int profileId, int memberId)
        {
            var memberProfileRelationship = _memberProfileRelationshipDataService.Get(memberId, profileId);

            _memberProfileRelationshipDataService.Delete(memberProfileRelationship);

            var profile = _profileDataService.Get(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        public async Task<Dto.Profile> RemoveOwnerAsync(int profileId, int memberId)
        {
            var memberProfileRelationship = await _memberProfileRelationshipDataService.GetAsync(memberId, profileId);

            _memberProfileRelationshipDataService.Delete(memberProfileRelationship);

            var profile = await _profileDataService.GetAsync(profileId);

            return _mapper.Map<Dto.Profile>(profile);
        }

        #endregion

        #region Helper Methods

        private Dto.Profile SetActivation(
             string firebaseId,
             int profileId,
             bool isActive)
        {
            var profile = _profileDataService.Get(profileId);

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
            var profile = await _profileDataService.GetAsync(profileId);

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
                MemberId = _memberDataService.GetByFirebaseId(firebaseId).Id
            };

            profile.MemberProfileRelationships.Add(memberProfileRelationship);

            return profile;
        }

        #endregion
    }
}
