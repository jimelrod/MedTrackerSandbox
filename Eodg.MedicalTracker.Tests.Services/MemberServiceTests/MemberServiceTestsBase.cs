using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Services.Interfaces;
using System;

namespace Eodg.MedicalTracker.Tests.Services.MemberServiceTests
{
    public abstract class MemberServiceTestsBase : ServiceTestsBase
    {
        public MemberServiceTestsBase(ServiceFixture serviceFixture)
            : base(serviceFixture)
        {
           MemberService = GetService<IMemberService>();
        }
        
        protected IMemberService MemberService { get; private set; }

        protected static Member GenerateDomainMember()
        {
            return new Member
            {
                FirebaseId = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                DisplayName = Guid.NewGuid().ToString()
            };
        }
    }
}