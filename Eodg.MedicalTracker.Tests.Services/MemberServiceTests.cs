using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Eodg.MedicalTracker.Tests.Services
{
    public class MemberServiceTests : IClassFixture<ServiceFixture>
    {
        private readonly IMemberService _memberService;

        public MemberServiceTests(ServiceFixture serviceFixture)
        {
            _memberService = serviceFixture.ServiceProvider.GetService<IMemberService>();
        }

        [Fact]
        public void CanAddMember()
        {
            var firebaseId = "CanAddMember";
            var email = "CanAddMember@test.com";

            var member = _memberService.Add(firebaseId, email);

            Assert.Equal(firebaseId, member.FirebaseId);
            Assert.Equal(email, member.Email);
        }

        [Fact]
        public async Task CanAddMemberAsync()
        {
            var firebaseId = "CanAddMemberAsync";
            var email = "CanAddMemberAsync@test.com";

            var member = await _memberService.AddAsync(firebaseId, email);

            Assert.Equal(firebaseId, member.FirebaseId);
            Assert.Equal(email, member.Email);
        }

        // [Theory]
        // [InlineData(3)]
        // [InlineData(5)]
        // public void MyFirstTheory(int value)
        // {
        //     Assert.True(IsOdd(value));
        // }
    }
}
