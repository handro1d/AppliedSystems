using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using Microsoft.AspNet.Identity;
using Moq;

namespace AppliedSystems.Tests.Common
{
    public sealed class FakeUserStore
    {
        private readonly Mock<IUserStore<User, int>> _store;
        private readonly Mock<IUserPasswordStore<User, int>> _passwordStore;
        private readonly Mock<IUserEmailStore<User, int>> _emailStore;
        private readonly Mock<IUserLockoutStore<User, int>> _lockoutStore;
        private readonly Mock<IUserTwoFactorStore<User, int>> _twoFactorStore;

        public FakeUserStore()
        {
            _store = new Mock<IUserStore<User, int>>();
            _passwordStore = _store.As<IUserPasswordStore<User, int>>();
            _emailStore = _store.As<IUserEmailStore<User, int>>();
            _lockoutStore = _store.As<IUserLockoutStore<User, int>>();
            _twoFactorStore = _store.As<IUserTwoFactorStore<User, int>>();
        }

        public FakeUserStore WithUser(User user)
        {
            _store
                .Setup(s => s.FindByNameAsync(user.UserName))
                .ReturnsAsync(user);

            _store
             .Setup(s => s.FindByIdAsync(user.Id))
             .ReturnsAsync(user);

            _passwordStore
                .Setup(s => s.HasPasswordAsync(user))
                .ReturnsAsync(!string.IsNullOrEmpty(user.PasswordHash));

            _passwordStore
                .Setup(s => s.SetPasswordHashAsync(user, It.IsAny<string>()))
                .Callback((User u, string newPassHash) => { u.PasswordHash = newPassHash; })
                .Returns(Task.FromResult(0));

            _passwordStore
                .Setup(s => s.GetPasswordHashAsync(user))
                .ReturnsAsync(user.PasswordHash);

            _emailStore
                .Setup(s => s.FindByEmailAsync(user.Email))
                .ReturnsAsync(user);

            return this;
        }

        public FakeUserStore WithAbilityToCreateUser(User userToReturn = null)
        {
            _store
                .Setup(s => s.CreateAsync(It.IsAny<User>()))
                .Returns((User user) => Task.FromResult(userToReturn ?? user));

            return this;
        }

        public Mock<IUserStore<User, int>> Build()
        {
            _lockoutStore
                .Setup(ls => ls.GetLockoutEnabledAsync(It.IsAny<User>()))
                .ReturnsAsync(false);

            _lockoutStore
                .Setup(ls => ls.GetAccessFailedCountAsync(It.IsAny<User>()))
                .ReturnsAsync(0);

            _twoFactorStore
                .Setup(tfs => tfs.GetTwoFactorEnabledAsync(It.IsAny<User>()))
                .ReturnsAsync(false);

            return _store;
        }
    }
}
