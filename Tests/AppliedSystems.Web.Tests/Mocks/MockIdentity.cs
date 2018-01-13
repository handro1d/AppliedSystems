using System.Security.Principal;

namespace AppliedSystems.Web.Tests
{
    internal sealed class MockIdentity : GenericIdentity
    {
        public new bool IsAuthenticated { get; }

        public MockIdentity(string name)
            : base(name)
        {
        }

        public MockIdentity(string name, string type)
            : base(name, type)
        {
        }

        public MockIdentity(GenericIdentity identity)
            : base(identity)
        {
        }

        public MockIdentity(bool isAuthenticated = false)
            : base(isAuthenticated ? "Test Identity" : string.Empty)
        {
            IsAuthenticated = isAuthenticated;
        }
    }
}
