using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data
{
    public class MemberDataService : DataService<Member>, IMemberDataService
    {
        public MemberDataService(MedicalTrackerDbContext dbContext)
            : base(dbContext)
        {
        }

        public Member GetByFirebaseId(string firebaseId)
        {
            Member member;

            try
            {
                member = DbContext.Members.Single(m => m.FirebaseId == firebaseId);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"Member not found. Firebase Id: {firebaseId}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return member;
        }

        public async Task<Member> GetByFirebaseIdAsync(string firebaseId)
        {
            Member member;

            try
            {
                member = await DbContext.Members.SingleAsync(m => m.FirebaseId == firebaseId);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"Member not found. Firebase Id: {firebaseId}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return member;
        }
    }
}