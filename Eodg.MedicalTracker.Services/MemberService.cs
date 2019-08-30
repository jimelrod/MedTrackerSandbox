using AutoMapper;
using Eodg.MedicalTracker.Dto;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    // TODO: We'll need some try/catch and custom exceptions...
    public class MemberService : ResourceService<Domain.Member>, IMemberService
    {
        public MemberService(MedicalTrackerDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public Member Get(int id)
        {
            var member = GetEntity(id);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(int id)
        {
            var member = await GetEntityAsync(id);

            return Mapper.Map<Member>(member);
        }

        public Member Get(string firebaseId)
        {
            var member = GetEntity(m => m.FirebaseId == firebaseId);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(string firebaseId)
        {
            var member = await GetEntityAsync(m => m.FirebaseId == firebaseId);

            return Mapper.Map<Member>(member);
        }

        public Member Add(string firebaseId, string email, string displayName)
        {
            var member = GenerateMember(firebaseId, email, displayName);

            member = AddEntity(member);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> AddAsync(string firebaseId, string email, string displayName)
        {
            var member = GenerateMember(firebaseId, email, displayName);

            member = await AddEntityAsync(member);

            return Mapper.Map<Member>(member);
        }

        public Member Update(string firebaseId, string email, string displayName)
        {
            var member = GetEntity(m => m.FirebaseId == firebaseId);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            member = UpdateEntity(member);

            return Mapper.Map<Member>(member);

        }

        public async Task<Member> UpdateAsync(string firebaseId, string email, string displayName)
        {
            var member = await GetEntityAsync(m => m.FirebaseId == firebaseId);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            member = await UpdateEntityAsync(member);

            return Mapper.Map<Member>(member);
        }

        public Member Update(int id, string email, string displayName)
        {
            var member = GetEntity(id);

            member.Email = email;
            member.DisplayName = displayName;
            member.ModifiedOn = DateTime.Now;

            member = UpdateEntity(member);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> UpdateAsync(int id, string email, string displayName)
        {
            var member = await GetEntityAsync(id);

            member.Email = email;
            member.DisplayName = displayName;

            member = await UpdateEntityAsync(member);

            return Mapper.Map<Member>(member);
        }

        public Member Deactivate(int id)
        {
            var member = GetEntity(id);

            member = SetActivation(member, false);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> DeactivateAsync(int id)
        {
            var member = await GetEntityAsync(id);

            member = await SetActivationAsync(member, false);

            return Mapper.Map<Member>(member);
        }

        public Member Deactivate(string firebaseId)
        {
            var member = GetEntity(m => m.FirebaseId == firebaseId);

            member = SetActivation(member, false);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> DeactivateAsync(string firebaseId)
        {
            var member = await GetEntityAsync(m => m.FirebaseId == firebaseId);

            member = await SetActivationAsync(member, false);

            return Mapper.Map<Member>(member);
        }

        public Member Activate(int id)
        {
            var member = GetEntity(id);

            member = SetActivation(member, true);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> ActivateAsync(int id)
        {
            var member = await GetEntityAsync(id);

            member = await SetActivationAsync(member, true);

            return Mapper.Map<Member>(member);
        }

        public Member Activate(string firebaseId)
        {
            var member = GetEntity(m => m.FirebaseId == firebaseId);

            member = SetActivation(member, true);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> ActivateAsync(string firebaseId)
        {
            var member = await GetEntityAsync(m => m.FirebaseId == firebaseId);

            member = await SetActivationAsync(member, true);

            return Mapper.Map<Member>(member);
        }

        public void Delete(int id)
        {
            var member = GetEntity(id);

            DeleteEntity(member);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await GetEntityAsync(id);

            await DeleteEntityAsync(member);
        }

        public void Delete(string firebaseId)
        {
            var member = GetEntity(m => m.FirebaseId == firebaseId);

            DeleteEntity(member);
        }

        public async Task DeleteAsync(string firebaseId)
        {
            var member = await GetEntityAsync(m => m.FirebaseId == firebaseId);

            await DeleteEntityAsync(member);
        }

        #region Helper Methods

        private Domain.Member SetActivation(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            return UpdateEntity(member);
        }

        private async Task<Domain.Member> SetActivationAsync(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            return await UpdateEntityAsync(member);
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