using System.Linq;
using AppliedSystems.Common;
using AppliedSystems.Domain;
using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Services
{
    public sealed class InsurancePremiumCalculationOccupationHandler : InsurancePremiumCalulcationHandler
    {
        public override double HandleRequest(InsurancePolicy policy, double currentPremium)
        {
            if (policy.Drivers.Any(d => EnumExtensions.Parse<Occupation>(d.Occupation.Code) == Occupation.Chauffeur))
            {
                currentPremium = currentPremium * 1.1;
            }

            if (policy.Drivers.Any(d => EnumExtensions.Parse<Occupation>(d.Occupation.Code) == Occupation.Accountant))
            {
                currentPremium = currentPremium * 0.9;
            }

            return Successor?.HandleRequest(policy, currentPremium) ?? currentPremium;
        }
    }
}
