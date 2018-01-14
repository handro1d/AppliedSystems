using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using Moq;

namespace AppliedSystems.Web.Tests
{
    internal sealed class FakePolicyService
    {
        private readonly Mock<IPolicyService> _service;

        public FakePolicyService()
        {
            _service = new Mock<IPolicyService>();
        }

        public Mock<IPolicyService> Build()
        {
            _service
                .Setup(s => s.AddInsurancePolicyApplication(It.IsAny<InsurancePolicy>()))
                .Returns((InsurancePolicy policy) => policy);

            return _service;
        }
    }
}
