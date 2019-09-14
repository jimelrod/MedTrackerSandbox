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
    public class MemberProfileRelationshipDataService : DataService<MemberProfileRelationship>, IMemberProfileRelationshipDataService
    {
        public MemberProfileRelationshipDataService(MedicalTrackerDbContext dbContext)
            : base(dbContext)
        {
        }

        public MemberProfileRelationship Get(int memberId, int profileId)
        {
            MemberProfileRelationship memberProfileRelationship;

            try
            {
                memberProfileRelationship =
                    DbContext
                        .MemberProfileRelationships
                        .Single(mp => mp.MemberId == memberId && mp.ProfileId == profileId);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"MemberProfileRelationship not found. ProfileId: {profileId}, MemberId: {memberId}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return memberProfileRelationship;
        }

        public async Task<MemberProfileRelationship> GetAsync(int memberId, int profileId)
        {
            MemberProfileRelationship memberProfileRelationship;

            try
            {
                memberProfileRelationship =
                    await 
                        DbContext
                            .MemberProfileRelationships
                            .SingleAsync(mp => mp.MemberId == memberId && mp.ProfileId == profileId);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"MemberProfileRelationship not found. ProfileId: {profileId}, MemberId: {memberId}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return memberProfileRelationship;
        }
    }
}