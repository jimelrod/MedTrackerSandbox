using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Eodg.MedicalTracker.Api.Authorization
{
    public class MemberSpecificAuthorizationRequirement : IAuthorizationRequirement
    {
        public MemberSpecificAuthorizationRequirement(IEnumerable<string> firebaseIds)
        {
            FirebaseIds = firebaseIds;
        }

        public IEnumerable<string> FirebaseIds { get; private set; }

        public bool IsMet(AuthorizationHandlerContext context)
        {
            Func<string, bool> ValidateFirebaseId = (string firebaseId) =>
            {
                return
                    context
                        .User
                        .HasClaim(ClaimTypes.NameIdentifier, firebaseId);
            };

            return FirebaseIds.Any(ValidateFirebaseId);
        }
    }
}