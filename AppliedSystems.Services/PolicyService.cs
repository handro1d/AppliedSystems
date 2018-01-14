using System;
using AppliedSystems.Common;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Services
{
    public sealed class PolicyService : IPolicyService
    {
        private readonly IAppliedSystemsRepository<InsurancePolicy> _insurancePolicyRepository;

        public PolicyService(IAppliedSystemsRepository<InsurancePolicy> insurancePolicyRepository)
        {
            if (insurancePolicyRepository == null)
            {
                throw new ArgumentNullException("insurancePolicyRepository");
            }

            _insurancePolicyRepository = insurancePolicyRepository;
        }

        public InsurancePolicy AddInsurancePolicyApplication(InsurancePolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }

            TransactionHelper.InvokeTransaction(() =>
            {
                policy = _insurancePolicyRepository.Add(policy);
            });

            return policy;
        }

        public double CalculatePremium(InsurancePolicy policy, double startingPremium)
        {
            var occupationHandler = new InsurancePremiumCalculationOccupationHandler();
            var youngestDriverHandler = new InsurancePremiumCalculationYoungestDriverHandler();
            var claimsHandler = new InsurancePremiumCalculationClaimsHandler();

            youngestDriverHandler.SetSuccessor(claimsHandler);
            occupationHandler.SetSuccessor(youngestDriverHandler);

            return occupationHandler.HandleRequest(policy, startingPremium);
        }

        public InsurancePolicy UpdateInsurancePolicy(InsurancePolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }

            TransactionHelper.InvokeTransaction(() =>
            {
                policy = _insurancePolicyRepository.Update(policy, policy.Id);
            });

            return policy;
        }
    }
}
