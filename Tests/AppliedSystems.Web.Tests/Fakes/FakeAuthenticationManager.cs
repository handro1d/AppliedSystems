using System.Security.Claims;
using Microsoft.Owin.Security;
using Moq;

namespace AppliedSystems.Web.Tests
{
    internal sealed class FakeAuthenticationManager
    {
        private readonly Mock<IAuthenticationManager> _manager;

        public FakeAuthenticationManager()
        {
            _manager = new Mock<IAuthenticationManager>();
        }

        public FakeAuthenticationManager WithUser(ClaimsPrincipal user)
        {
            _manager
                .SetupGet(m => m.User)
                .Returns(user);

            return this;
        }

        public FakeAuthenticationManager WithUser(ClaimsIdentity identity)
        {
            _manager
                .SetupGet(m => m.User)
                .Returns(new ClaimsPrincipal(identity));

            return this;
        }

        public Mock<IAuthenticationManager> Build()
        {
            return _manager;
        }
    }
}
