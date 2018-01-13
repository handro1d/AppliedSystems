using AppliedSystems.Domain.DAO;
using AppliedSystems.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AppliedSystems.Web.Tests
{
    internal sealed class AccountControllerBuilder
    {
        private IAuthenticationManager _authenticationManager;
        private SignInManager<User, int> _signInManager;
        private UserManager<User, int> _userManager;

        public AccountControllerBuilder()
        {
            _authenticationManager = new FakeAuthenticationManager().Build().Object;
            _signInManager = new MockSignInManager();
            _userManager = new MockUserManager();
        }

        public AccountControllerBuilder WithAuthenticationManager(IAuthenticationManager manager)
        {
            _authenticationManager = manager;
            return this;
        }

        public AccountControllerBuilder WithSignInManager(SignInManager<User, int> signInManager)
        {
            _signInManager = signInManager;
            return this;
        }

        public AccountControllerBuilder WithUserManager(UserManager<User, int> userManager)
        {
            _userManager = userManager;
            return this;
        }

        public AccountController Build()
        {
            return new AccountController(_authenticationManager, _signInManager, _userManager);
        }
    }
}
