using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eodg.MedicalTracker.Tests.Services
{
    public class DataUtilityService
    {
        #region Seed Dictionaries

        private readonly Dictionary<int, bool> _isActiveByMemberId = new Dictionary<int, bool>
        {
            {1, true},
            {2, true},
            {3, true},
            {4, false},
            {5, true},
            {6, false},
            {7, false},
            {8, true}
        };

        #endregion

        private readonly MedicalTrackerDbContext _dbContext;

        public DataUtilityService(MedicalTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _isActiveByMemberId.Keys.ToList().ForEach(key =>
            {
                _dbContext.Add(GenerateMember(key, _isActiveByMemberId[key]));
            });

            _dbContext.SaveChanges();

            // TODO: Refactor this...
            var profile = new Profile
            {
                DisplayName = "Profile Test 1",
                Notes = "Here's some notes...",
                IsActive = true,
                CreatedBy = "FirebaseId1",
                CreatedOn = DateTime.Now,
                ModifiedBy = "FirebaseId1",
                ModifiedOn = DateTime.Now
            };

            _dbContext.Profiles.Add(profile);

            _dbContext.SaveChanges();

            var relationships = new List<MemberProfileRelationship>
            {
                new MemberProfileRelationship
                {
                    MemberId = 1,
                    ProfileId = profile.Id
                },
                new MemberProfileRelationship
                {
                    MemberId = 2,
                    ProfileId = profile.Id
                }
            };

            _dbContext.AddRange(relationships);

            _dbContext.SaveChanges();
        }

        private static Member GenerateMember(int id, bool isActive)
        {
            return new Member
            {
                FirebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}",
                Email = $"{MemberProperty.Email.ToString()}{id}",
                DisplayName = $"{MemberProperty.DisplayName.ToString()}{id}",
                IsActive = isActive,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }
    }
}