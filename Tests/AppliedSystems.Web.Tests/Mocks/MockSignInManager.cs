using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AppliedSystems.Web.Tests
{
    internal sealed class MockSignInManager : SignInManager<User, int>
    {
        private SignInStatus _passwordSigninStatus;

        public MockSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager) 
            : base(userManager, authenticationManager)
        {
            _passwordSigninStatus = SignInStatus.Failure;
        }

        public MockSignInManager()
            : base(new MockUserManager(), new FakeAuthenticationManager().Build().Object)
        {
            _passwordSigninStatus = SignInStatus.Failure;
        }

        public MockSignInManager WithPasswordSignInStatus(SignInStatus signInStatus)
        {
            _passwordSigninStatus = signInStatus;
            return this;
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return Task.FromResult(_passwordSigninStatus);
        }
    }
}
