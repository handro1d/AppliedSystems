using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Services
{
    public abstract class InsurancePremiumCalulcationHandler
    {
        protected InsurancePremiumCalulcationHandler Successor;

        public abstract double HandleRequest(InsurancePolicy policy, double currentPremium);

        public void SetSuccessor(InsurancePremiumCalulcationHandler successor)
        {
            Successor = successor;
        }
    }
}
