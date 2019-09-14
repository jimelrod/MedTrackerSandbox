using Eodg.MedicalTracker.Services.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Eodg.MedicalTracker.Tests.Services.MemberServiceTests
{
    public class MemberServiceAddTests : MemberServiceTestsBase, IClassFixture<ServiceFixture>
    {
        public MemberServiceAddTests(ServiceFixture serviceFixture)
            : base(serviceFixture)
        {
        }

        #region Add

        [Fact]
        public void Add_ValidDataWithDisplayName_ReturnsMember()
        {
            var inputMember = GenerateDomainMember();

            var outputMember = MemberService.Add(inputMember.FirebaseId, inputMember.Email, inputMember.DisplayName);

            Assert.True(outputMember.Id > 0);
            Assert.Equal(inputMember.Email, outputMember.Email);
            Assert.Equal(inputMember.FirebaseId, outputMember.FirebaseId);
            Assert.Equal(inputMember.DisplayName, outputMember.DisplayName);
        }

        [Fact]
        public void Add_ValidDataWithoutDisplayName_ReturnsMember()
        {
            var inputMember = GenerateDomainMember();

            var outputMember = MemberService.Add(inputMember.FirebaseId, inputMember.Email);

            Assert.True(outputMember.Id > 0);
            Assert.Equal(inputMember.Email, outputMember.Email);
            Assert.Equal(inputMember.FirebaseId, outputMember.FirebaseId);
            Assert.True(string.IsNullOrEmpty(outputMember.DisplayName));
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
        public void Add_ExistingFirebaseIdNotInContext_ThrowsResourceNotAddedException(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            // Detach existing entity from context
            Detach<Domain.Member>(m => m.FirebaseId == firebaseId);

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            Assert.Throws<ResourceNotAddedException>(() =>
            {
                MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
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
        public void Add_ExistingFirebaseIdInContext_ThrowsResourceNotAddedException(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            Assert.Throws<ResourceNotAddedException>(() =>
            {
                MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
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
        public void Add_ExistingEmailNotInContext_ThrowsResourceNotAddedException(int id)
        {
            var email = $"{MemberProperty.Email.ToString()}{id}";

            // Detach existing entity from context
            Detach<Domain.Member>(m => m.Email == email);

            var member = GenerateDomainMember();
            member.Email = email;

            Assert.Throws<ResourceNotAddedException>(() =>
            {
                MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
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
        public void Add_ExistingEmailInContext_ThrowsResourceNotAddedException(int id)
        {
            var email = $"{MemberProperty.Email.ToString()}{id}";

            var member = GenerateDomainMember();
            member.Email = email;

            Assert.Throws<ResourceNotAddedException>(() =>
            {
                MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
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
        public void Add_ExistingFirebaseId_NextSaveWorks(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            try
            {
                var failedMember = MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
            }
            catch (ResourceNotAddedException)
            {
                member = GenerateDomainMember();

                var successfulMember = MemberService.Add(member.FirebaseId, member.Email, member.DisplayName);
            }
        }

        #endregion

        #region AddAsync

        [Fact]
        public async Task AddAsync_ValidDataWithDisplayName_ReturnsMember()
        {
            var inputMember = GenerateDomainMember();

            var outputMember = await MemberService.AddAsync(inputMember.FirebaseId, inputMember.Email, inputMember.DisplayName);

            Assert.True(outputMember.Id > 0);
            Assert.Equal(inputMember.Email, outputMember.Email);
            Assert.Equal(inputMember.FirebaseId, outputMember.FirebaseId);
            Assert.Equal(inputMember.DisplayName, outputMember.DisplayName);
        }

        [Fact]
        public async Task AddAsync_ValidDataWithoutDisplayName_ReturnsMember()
        {
            var inputMember = GenerateDomainMember();

            var outputMember = await MemberService.AddAsync(inputMember.FirebaseId, inputMember.Email);

            Assert.True(outputMember.Id > 0);
            Assert.Equal(inputMember.Email, outputMember.Email);
            Assert.Equal(inputMember.FirebaseId, outputMember.FirebaseId);
            Assert.True(string.IsNullOrEmpty(outputMember.DisplayName));
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
        public async Task AddAsync_ExistingFirebaseIdNotInContext_ThrowsResourceNotAddedException(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            // Detach existing entity from context
            Detach<Domain.Member>(m => m.FirebaseId == firebaseId);

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            await Assert.ThrowsAsync<ResourceNotAddedException>(() =>
            {
                return MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
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
        public async Task AddAsync_ExistingFirebaseIdInContext_ThrowsResourceNotAddedException(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            await Assert.ThrowsAsync<ResourceNotAddedException>(() =>
            {
                return MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
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
        public async Task AddAsync_ExistingEmailNotInContext_ThrowsResourceNotAddedException(int id)
        {
            var email = $"{MemberProperty.Email.ToString()}{id}";

            // Detach existing entity from context
            Detach<Domain.Member>(m => m.Email == email);

            var member = GenerateDomainMember();
            member.Email = email;

            await Assert.ThrowsAsync<ResourceNotAddedException>(() =>
            {
                return MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
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
        public async Task AddAsync_ExistingEmailInContext_ThrowsResourceNotAddedException(int id)
        {
            var email = $"{MemberProperty.Email.ToString()}{id}";

            var member = GenerateDomainMember();
            member.Email = email;

            await Assert.ThrowsAsync<ResourceNotAddedException>(() =>
            {
                return MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
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
        public async Task AddAsync_ExistingFirebaseId_NextSaveWorks(int id)
        {
            var firebaseId = $"{MemberProperty.FirebaseId.ToString()}{id}";

            var member = GenerateDomainMember();
            member.FirebaseId = firebaseId;

            try
            {
                var failedMember = await MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
            }
            catch (ResourceNotAddedException)
            {
                member = GenerateDomainMember();

                var successfulMember = await MemberService.AddAsync(member.FirebaseId, member.Email, member.DisplayName);
            }
        }

        #endregion
    }
}