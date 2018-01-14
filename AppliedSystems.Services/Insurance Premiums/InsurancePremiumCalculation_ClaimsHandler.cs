using System.Linq;
using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Services
{
    public sealed class InsurancePremiumCalculationClaimsHandler : InsurancePremiumCalulcationHandler
    {
        public override double HandleRequest(InsurancePolicy policy, double currentPremium)
        {
            var policyClaims = policy.Drivers.SelectMany(d => d.Claims).ToList();

            foreach (var driverClaim in policyClaims.Where(c => c.DateOfClaim > policy.StartDate.AddYears(-1)))
            {
                currentPremium = currentPremium * 1.2;
            }

            foreach (var driverClaim in policyClaims.Where(c => c.DateOfClaim > policy.StartDate.AddYears(-5) && c.DateOfClaim < policy.StartDate.AddYears(-2)))
            {
                currentPremium = currentPremium * 1.1;
            }

            return Successor?.HandleRequest(policy, currentPremium) ?? currentPremium;
        }
    }
}
