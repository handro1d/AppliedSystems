using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using AppliedSystems.Services;
using AppliedSystems.Tests.Common;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Web.Tests
{
    internal sealed class MockUserManager : AppliedSystemsUserManager
    {
        private IdentityResult _creationResult;

        public MockUserManager(IUserStore<User, int> store, IAppliedSystemsPasswordHasher passwordHasher) 
            : base(store, passwordHasher)
        {
            _creationResult = IdentityResult.Failed();
        }

        public MockUserManager()
            : base(new FakeUserStore().Build().Object, new FakePasswordHasher().Build().Object)
        {
            _creationResult = IdentityResult.Failed();
        }

        public MockUserManager WithUserCreationResult(IdentityResult result)
        {
            _creationResult = result;
            return this;
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            return Task.FromResult(_creationResult);
        }
    }
}