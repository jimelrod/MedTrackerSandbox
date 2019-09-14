using Eodg.MedicalTracker.Domain;
using Eodg.MedicalTracker.Services.Interfaces;
using System;

namespace Eodg.MedicalTracker.Tests.Services.ProfileServiceTests
{
    public abstract class ProfileServiceTestsBase : ServiceTestsBase
    {
        public ProfileServiceTestsBase(ServiceFixture serviceFixture)
            : base(serviceFixture)
        {
            ProfileService = GetService<IProfileService>();
        }

        protected IProfileService ProfileService { get; private set; }

        // TODO: Figure out what this needs...
        protected static Profile GenerateDomainProfile()
        {
            throw new NotImplementedException();
        }
    }
}