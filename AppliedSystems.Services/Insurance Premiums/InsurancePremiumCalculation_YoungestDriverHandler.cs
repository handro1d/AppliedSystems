using AppliedSystems.Common;
using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Services
{
    public sealed class InsurancePremiumCalculationYoungestDriverHandler : InsurancePremiumCalulcationHandler
    {
        public override double HandleRequest(InsurancePolicy policy, double currentPremium)
        {
            var youngestDriver = policy.YoungestDriver;

            if (youngestDriver != null)
            {
                var ageOnStartDate = youngestDriver.DateOfBirth.CalculateAgeOnDate(policy.StartDate);

                if (ageOnStartDate >= 21 && ageOnStartDate <= 25)
                {
                    currentPremium = currentPremium * 1.2;

                    return Successor?.HandleRequest(policy, currentPremium) ?? currentPremium;
                }

                if (ageOnStartDate >= 26 && ageOnStartDate <= 75)
                {
                    currentPremium = currentPremium * 0.9;

                    return Successor?.HandleRequest(policy, currentPremium) ?? currentPremium;
                }
            }

            return Successor?.HandleRequest(policy, currentPremium) ?? currentPremium;
        }
    }
}
