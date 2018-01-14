using AppliedSystems.Interfaces;
using AppliedSystems.Web.Controllers;
using Microsoft.Owin.Security;

namespace AppliedSystems.Web.Tests
{
    internal sealed class PolicyControllerBuilder
    {
        private IReferenceService _referenceService;
        private IPolicyService _policyService;
        private IAuthenticationManager _authenticationManager;

        public PolicyControllerBuilder()
        {
            _referenceService = new FakeReferenceService().Build().Object;
            _policyService = new FakePolicyService().Build().Object;
            _authenticationManager = new FakeAuthenticationManager().Build().Object;
        }

        public PolicyControllerBuilder WithReferenceService(IReferenceService service)
        {
            _referenceService = service;
            return this;
        }

        public PolicyControllerBuilder WithPolicyService(IPolicyService service)
        {
            _policyService = service;
            return this;
        }

        public PolicyControllerBuilder WithAuthenticationManager(IAuthenticationManager manager)
        {
            _authenticationManager = manager;
            return this;
        }

        public PolicyController Build()
        {
            return new PolicyController(_referenceService, _policyService, _authenticationManager);
        }
    }
}
