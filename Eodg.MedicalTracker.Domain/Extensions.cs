using Eodg.MedicalTracker.Domain.Interfaces;
using System;

namespace Eodg.MedicalTracker.Domain
{
    public static class Extensions
    {
        public static void AddTimestamp(this ITimestampable entity, string firebaseId, bool updateCreated = false)
        {
            var now = DateTime.Now;

            entity.ModifiedBy = firebaseId;
            entity.ModifiedOn = now;

            if (updateCreated)
            {
                entity.CreatedBy = firebaseId;
                entity.CreatedOn = now;
            }
        }
    }
}