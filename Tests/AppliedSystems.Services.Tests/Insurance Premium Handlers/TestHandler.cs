using AppliedSystems.Domain.DAO;

namespace AppliedSystems.Services.Tests
{
    internal sealed class TestHandler : InsurancePremiumCalulcationHandler
    {
        private double _value;

        public TestHandler()
        {
            _value = 123;
        }

        public TestHandler WithReturnValue(double value)
        {
            _value = value;
            return this;
        }

        public override double HandleRequest(InsurancePolicy policy, double currentPremium)
        {
            return _value;
        }
    }
}
