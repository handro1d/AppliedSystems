using AppliedSystems.Interfaces;
using Moq;

namespace AppliedSystems.Web.Tests
{
    internal sealed class FakeReferenceService
    {
        private readonly Mock<IReferenceService> _service;

        public FakeReferenceService()
        {
            _service = new Mock<IReferenceService>();
        }

        public Mock<IReferenceService> Build()
        {
            return _service;
        }
    }
}
