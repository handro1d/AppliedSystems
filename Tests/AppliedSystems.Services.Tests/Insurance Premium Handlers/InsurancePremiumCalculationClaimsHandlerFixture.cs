using System;
using System.Collections.Generic;
using AppliedSystems.Domain.DAO;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class InsurancePremiumCalculationClaimsHandlerFixture
    {
        private InsurancePremiumCalculationClaimsHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _handler = new InsurancePremiumCalculationClaimsHandler();
        }

        [Test]
        public void HandleRequest_ShouldReturnPremiumIfNoClaims()
        {
            var policy = new InsurancePolicy();

            var returnedPremium = _handler.HandleRequest(policy, 100);

            Assert.AreEqual(100, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldCallSuccessor()
        {
            // Setup dependencies
            var policy = new InsurancePolicy();

            var testHandler = new TestHandler()
                .WithReturnValue(456);

            var handler = new InsurancePremiumCalculationClaimsHandler();
            handler.SetSuccessor(testHandler);

            // Call HandleRequest
            var returnedPremium = handler.HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(456, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldIncreasePremiumByTwentyPercentForRecentClaims()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim{ DateOfClaim = DateTime.UtcNow.AddMonths(-1) },
                            new DriverClaim{ DateOfClaim = DateTime.UtcNow.AddMonths(-7) }
                        }
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationClaimsHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(144, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldIncreasePremiumByTenPercentForLessRecentClaims()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim{ DateOfClaim = DateTime.UtcNow.AddMonths(-26) },
                            new DriverClaim{ DateOfClaim = DateTime.UtcNow.AddMonths(-37) },
                            new DriverClaim{ DateOfClaim = DateTime.UtcNow.AddYears(-10) }
                        }
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationClaimsHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual((double)121, Math.Round(returnedPremium, 2));
        }
    }
}
