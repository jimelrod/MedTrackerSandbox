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