using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Services.Tests
{
    internal sealed class PolicyServiceBuilder
    {
        private IAppliedSystemsRepository<InsurancePolicy> _insurancePolicyRepository;

        public PolicyServiceBuilder()
        {
            _insurancePolicyRepository = new FakeRepository<InsurancePolicy>().Build().Object;
        }

        public PolicyServiceBuilder WithInsurancePolicyRepository(IAppliedSystemsRepository<InsurancePolicy> repository)
        {
            _insurancePolicyRepository = repository;
            return this;
        }

        public PolicyService Build()
        {
            return new PolicyService(_insurancePolicyRepository);
        }
    }
}
