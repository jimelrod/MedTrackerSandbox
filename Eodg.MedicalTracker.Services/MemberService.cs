using AutoMapper;
using Eodg.MedicalTracker.Dto;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberDataService _memberDataService;
        private readonly IMapper _mapper;

        public MemberService(IMemberDataService memberDataService, IMapper mapper)
        {
            _memberDataService = memberDataService;
            _mapper = mapper;
        }

        public Member Get(int id)
        {
            var member = _memberDataService.Get(id);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(int id)
        {
            var member = await _memberDataService.GetAsync(id);

            return _mapper.Map<Member>(member);
        }

        public Member Get(string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            return _mapper.Map<Member>(member);
        }

        public Member Add(string firebaseId, string email, string displayName)
        {
            var member = GenerateMember(firebaseId, email, displayName);

            _memberDataService.Add(member);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> AddAsync(string firebaseId, string email, string displayName)
        {
            var member = GenerateMember(firebaseId, email, displayName);

            await _memberDataService.AddAsync(member);

            return _mapper.Map<Member>(member);
        }

        public Member Update(string firebaseId, string email, string displayName)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            _memberDataService.Update(member);

            return _mapper.Map<Member>(member);

        }

        public async Task<Member> UpdateAsync(string firebaseId, string email, string displayName)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            await _memberDataService.UpdateAsync(member);

            return _mapper.Map<Member>(member);
        }

        public Member Update(int id, string email, string displayName)
        {
            var member = _memberDataService.Get(id);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            _memberDataService.Update(member);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> UpdateAsync(int id, string email, string displayName)
        {
            var member = await _memberDataService.GetAsync(id);

            member.Email = email;
            member.DisplayName = displayName;

            await _memberDataService.UpdateAsync(member);

            return _mapper.Map<Member>(member);
        }

        public Member Deactivate(int id)
        {
            var member = _memberDataService.Get(id);

            SetActivation(member, false);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> DeactivateAsync(int id)
        {
            var member = await _memberDataService.GetAsync(id);

            await SetActivationAsync(member, false);

            return _mapper.Map<Member>(member);
        }

        public Member Deactivate(string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            SetActivation(member, false);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> DeactivateAsync(string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            await SetActivationAsync(member, false);

            return _mapper.Map<Member>(member);
        }

        public Member Activate(int id)
        {
            var member = _memberDataService.Get(id);

            SetActivation(member, true);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> ActivateAsync(int id)
        {
            var member = await _memberDataService.GetAsync(id);

            await SetActivationAsync(member, true);

            return _mapper.Map<Member>(member);
        }

        public Member Activate(string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            SetActivation(member, true);

            return _mapper.Map<Member>(member);
        }

        public async Task<Member> ActivateAsync(string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            await SetActivationAsync(member, true);

            return _mapper.Map<Member>(member);
        }

        public void Delete(int id)
        {
            var member = _memberDataService.Get(id);

            _memberDataService.Delete(member);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _memberDataService.GetAsync(id);

            await _memberDataService.DeleteAsync(member);
        }

        public void Delete(string firebaseId)
        {
            var member = _memberDataService.GetByFirebaseId(firebaseId);

            _memberDataService.Delete(member);
        }

        public async Task DeleteAsync(string firebaseId)
        {
            var member = await _memberDataService.GetByFirebaseIdAsync(firebaseId);

            await _memberDataService.DeleteAsync(member);
        }

        #region Helper Methods

        private void SetActivation(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            _memberDataService.Update(member);
        }

        private async Task SetActivationAsync(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            await _memberDataService.UpdateAsync(member);
        }

        private static Domain.Member GenerateMember(string firebaseId, string email, string displayName)
        {
            var now = DateTime.Now;

            var member = new Domain.Member
            {
                FirebaseId = firebaseId,
                Email = email,
                DisplayName = displayName,
                IsActive = true,
                CreatedOn = now,
                ModifiedOn = now
            };

            return member;
        }

        #endregion
    }
}