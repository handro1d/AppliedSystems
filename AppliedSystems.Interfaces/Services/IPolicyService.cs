using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Interfaces
{
    public interface IPolicyService
    {
        InsurancePolicy AddInsurancePolicyApplication(InsurancePolicy policy);

        double CalculatePremium(InsurancePolicy policy, double startingPremium);

        InsurancePolicy UpdateInsurancePolicy(InsurancePolicy policy);
    }
}
