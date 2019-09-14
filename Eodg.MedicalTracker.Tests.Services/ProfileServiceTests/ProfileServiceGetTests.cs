using System.Threading.Tasks;
using Xunit;

namespace Eodg.MedicalTracker.Tests.Services.ProfileServiceTests
{
    public class ProfileServiceGetTests : ProfileServiceTestsBase, IClassFixture<ServiceFixture>
    {
        public ProfileServiceGetTests(ServiceFixture serviceFixture)
            : base(serviceFixture)
        {
        }

        [Fact]
        public void Get_ValidId_DoesNotThrowError()
        {
            var profile = ProfileService.Get(1);
        }

        [Fact]
        public async Task GetAsync_ValidId_DoesNotThrowError()
        {
            var profile = await ProfileService.GetAsync(1);
        }
    }
}