using AutoMapper;
using Eodg.MedicalTracker.Dto;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    // TODO: We'll need some try/catch and custom exceptions...
    // TODO: Actually use the DisplayName property
    public class MemberService : ResourceService, IMemberService
    {
        public MemberService(MedicalTrackerDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public Member Get(int id)
        {
            var member = GetDomainMember(id);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(int id)
        {
            var member = await GetDomainMemberAsync(id);

            return Mapper.Map<Member>(member);
        }

        public Member Get(string firebaseId)
        {
            var member = GetDomainMember(firebaseId);

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> GetAsync(string firebaseId)
        {
            var member = await GetDomainMemberAsync(firebaseId);

            return Mapper.Map<Member>(member);
        }

        public Member Add(string firebaseId, string email)
        {
            var member = GenerateMember(firebaseId, email);

            DbContext.Members.Add(member);
            DbContext.SaveChanges();

            return Mapper.Map<Member>(member);
        }

        public async Task<Member> AddAsync(string firebaseId, string email)
        {
            var member = GenerateMember(firebaseId, email);

            DbContext.Members.Add(member);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<Member>(member);
        }

        public Member Deactivate(int id)
        {
            var member = GetDomainMember(id);

            return SetActivation(member, false);
        }

        public async Task<Member> DeactivateAsync(int id)
        {
            var member = await GetDomainMemberAsync(id);

            return await SetActivationAsync(member, false);
        }

        public Member Deactivate(string firebaseId)
        {
            var member = GetDomainMember(firebaseId);

            return SetActivation(member, false);
        }

        public async Task<Member> DeactivateAsync(string firebaseId)
        {
            var member = await GetDomainMemberAsync(firebaseId);

            return await SetActivationAsync(member, false);
        }

        public Member Activate(int id)
        {
            var member = GetDomainMember(id);

            return SetActivation(member, true);
        }

        public async Task<Member> ActivateAsync(int id)
        {
            var member = await GetDomainMemberAsync(id);

            return await SetActivationAsync(member, true);
        }

        public Member Activate(string firebaseId)
        {
            var member = GetDomainMember(firebaseId);

            return SetActivation(member, true);
        }

        public async Task<Member> ActivateAsync(string firebaseId)
        {
            var member = await GetDomainMemberAsync(firebaseId);

            return await SetActivationAsync(member, true);
        }

        public void Delete(int id)
        {
            var member = GetDomainMember(id);

            Delete(member);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await GetDomainMemberAsync(id);

            await DeleteAsync(member);
        }

        public void Delete(string firebaseId)
        {
            var member = GetDomainMember(firebaseId);

            Delete(member);
        }

        public async Task DeleteAsync(string firebaseId)
        {
            var member = await GetDomainMemberAsync(firebaseId);

            await DeleteAsync(member);
        }

        #region Helper Methods

        private Domain.Member GetDomainMember(int id)
        {
            return DbContext.Members.Find(id);
        }

        private async Task<Domain.Member> GetDomainMemberAsync(int id)
        {
            return await DbContext.Members.FindAsync(id);
        }

        private Domain.Member GetDomainMember(string firebaseId)
        {
            return
                DbContext
                    .Members
                    .SingleOrDefault(m => m.FirebaseId == firebaseId);
        }

        private async Task<Domain.Member> GetDomainMemberAsync(string firebaseId)
        {
            return
                await
                    DbContext
                        .Members
                        .SingleOrDefaultAsync(m => m.FirebaseId == firebaseId);
        }

        private Member SetActivation(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            DbContext.Update(member);
            DbContext.SaveChanges();

            return Mapper.Map<Member>(member);
        }

        private async Task<Member> SetActivationAsync(Domain.Member member, bool isActive)
        {
            member.IsActive = isActive;
            member.ModifiedOn = DateTime.Now;

            DbContext.Update(member);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<Member>(member);
        }

        private void Delete(Domain.Member member)
        {
            DbContext.Remove(member);
            DbContext.SaveChanges();
        }

        private async Task DeleteAsync(Domain.Member member)
        {
            DbContext.Remove(member);
            await DbContext.SaveChangesAsync();
        }

        private static Domain.Member GenerateMember(string firebaseId, string email)
        {
            var now = DateTime.Now;

            var member = new Domain.Member
            {
                FirebaseId = firebaseId,
                Email = email,
                IsActive = true,
                CreatedOn = now,
                ModifiedOn = now
            };

            return member;
        }

        #endregion
    }
}