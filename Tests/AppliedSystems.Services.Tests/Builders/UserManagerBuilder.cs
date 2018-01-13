using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using AppliedSystems.Tests.Common;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Services.Tests
{
    internal sealed class UserManagerBuilder
    {
        private IUserStore<User, int> _userStore;
        private IAppliedSystemsPasswordHasher _passwordHasher;

        public UserManagerBuilder()
        {
            _userStore = new FakeUserStore().Build().Object;
            _passwordHasher = new FakePasswordHasher().Build().Object;
        }

        public UserManagerBuilder WithUserStore(IUserStore<User, int> userStore)
        {
            _userStore = userStore;
            return this;
        }

        public UserManagerBuilder WithPasswordHasher(IAppliedSystemsPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            return this;
        }

        public AppliedSystemsUserManager Build()
        {
            return new AppliedSystemsUserManager(_userStore, _passwordHasher);
        }
    }
}
