using AppliedSystems.Domain.DAO;
using AppliedSystems.Tests.Common;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class AppliedSystemsUserManagerFixture
    {
        private UserManagerBuilder _userManager;

        [SetUp]
        public void SetUp()
        {
            _userManager = new UserManagerBuilder();
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfPasswordHasherIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _userManager.WithPasswordHasher(null).Build();
            }, "passwordHasher");
        }

        [Test]
        public void FindAsync_ShouldReturnNullIfNoUserFound()
        {
            // Setup dependencies
            var userStore = new FakeUserStore().Build();

            // Call FindAsync
            var returnedUser = _userManager
                .WithUserStore(userStore.Object).Build()
                .FindAsync("IDontExist" ,"password").Result;

            // Verify result
            Assert.IsNull(returnedUser);
        }

        [Test]
        public void FindAsync_ShouldReturnNullForIncorrectPassword()
        {
            // Setup dependencies
            var user = new User { UserName = "TestUser" };

            var userStore = new FakeUserStore()
                .WithUser(user).Build();

            var passwordHasher = new FakePasswordHasher()
                .WithResult(PasswordVerificationResult.Failed).Build();

            // Call FindAsync
            var returnedUser = _userManager
                .WithUserStore(userStore.Object)
                .WithPasswordHasher(passwordHasher.Object).Build()
                .FindAsync("TestUser", "TestPassword").Result;

            // Verify result
            Assert.IsNull(returnedUser);
        }

        [Test]
        public void FindAsync_ShouldReturnCorrectUser()
        {
            // Setup dependencies
            var user = new User { UserName = "TestUser" };

            var userStore = new FakeUserStore()
                .WithUser(user).Build();

            var passwordHasher = new FakePasswordHasher()
                .WithResult(PasswordVerificationResult.Success).Build();

            // Call FindAsync
            var returnedUser = _userManager
                .WithUserStore(userStore.Object)
                .WithPasswordHasher(passwordHasher.Object).Build()
                .FindAsync("TestUser", "TestPassword").Result;

            Assert.AreEqual(user, returnedUser);
        }

        [Test]
        public void CreateAsync_ShouldCreateUser()
        {
            // Setup dependencies
            var userStore = new FakeUserStore()
                .WithAbilityToCreateUser().Build();

            var passwordHasher = new FakePasswordHasher()
                .WithPasswordSalt("TestSalt").Build();

            var user = new User { UserName = "TestUser", PasswordHash = "TestPasswordHash" };

            // Call CreateAsync
            var returnedUser = _userManager
                .WithUserStore(userStore.Object)
                .WithPasswordHasher(passwordHasher.Object).Build()
                .CreateAsync(user).Result;

            // Verify Result
            passwordHasher.Verify(ph => ph.HashPassword("TestPasswordHash"), Times.Once);
            userStore.Verify(us => us.CreateAsync(It.Is<User>(u => u.UserName == "TestUser")), Times.Once);
        }
    }
}
