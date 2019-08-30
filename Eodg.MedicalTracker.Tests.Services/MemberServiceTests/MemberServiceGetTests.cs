using Eodg.MedicalTracker.Services.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Eodg.MedicalTracker.Tests.Services.MemberServiceTests
{
    public class MemberServiceGetTests : MemberServiceTestsBase, IClassFixture<ServiceFixture>
    {
        public MemberServiceGetTests(ServiceFixture serviceFixture)
            : base(serviceFixture)
        {
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void Get_ValidId_DoesNotThrowError(int id)
        {
            var member = MemberService.Get(id);
        }

        [Fact]
        public void Get_InvalidId_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>(() =>
            {
                MemberService.Get(-1);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task GetAsync_ValidId_DoesNotThrowError(int id)
        {
            var member = await MemberService.GetAsync(id);
        }

        [Fact]
        public async Task GetAsync_InvalidId_ThrowsResourceNotFoundException()
        {
            await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
            {
                return MemberService.GetAsync(-1);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void Get_ValidFirebaseId_DoesNotThrowError(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = MemberService.Get(firebaseId);
        }

        [Fact]
        public void Get_InvalidFirebaseId_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>(() =>
            {
                MemberService.Get(Guid.NewGuid().ToString());
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task GetAsync_ValidFirebaseId_DoesNotThrowError(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = await MemberService.GetAsync(firebaseId);
        }

        [Fact]
        public async Task GetAsync_InvalidFirebaseId_ThrowsResourceNotFoundException()
        {
            await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
            {
                return MemberService.GetAsync(Guid.NewGuid().ToString());
            });
        }
    }
}
