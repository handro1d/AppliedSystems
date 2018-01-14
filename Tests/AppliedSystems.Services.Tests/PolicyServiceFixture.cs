using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Tests.Common;
using Moq;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class PolicyServiceFixture
    {
        private PolicyServiceBuilder _service;

        [SetUp]
        public void SetUp()
        {
            _service = new PolicyServiceBuilder();
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfInsurancePolicyRepositoryIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _service.WithInsurancePolicyRepository(null).Build();
            });
        }

        [Test]
        public void AddInsurancePolicyApplication_ShouldThrowExceptionIfPolicyIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _service.Build().AddInsurancePolicyApplication(null);
            }, "policy");
        }

        [Test]
        public void AddInsurancePolicyApplication_ShouldAddPolicy()
        {
            // Setup dependencies
            var policyRepository = new FakeRepository<InsurancePolicy>().Build();
            var policy = new InsurancePolicy { CreatedDate = DateTime.UtcNow, StartDate = DateTime.UtcNow.AddDays(14) };

            // Call AddInsurancePolicyApplication
            _service
                .WithInsurancePolicyRepository(policyRepository.Object).Build()
                .AddInsurancePolicyApplication(policy);

            // Verify result
            policyRepository.Verify(r => r.Add(policy), Times.Once);
        }
    }
}
