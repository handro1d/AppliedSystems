using System;
using System.Collections.Generic;
using AppliedSystems.Domain.DAO;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class InsurancePremiumCalculationYoungestDriverHandlerFixture
    {
        [Test]
        public void HandleRequest_ShouldReturnPremiumIfYoungestDriverDoesNotMatchCriteria()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-78)
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationYoungestDriverHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(100, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldCallSuccessor()
        {
            // Setup dependencies
            var policy = new InsurancePolicy();

            var testHandler = new TestHandler()
                .WithReturnValue(657);

            var handler = new InsurancePremiumCalculationYoungestDriverHandler();
            handler.SetSuccessor(testHandler);

            // Call HandleRequest
            var returnedPremium = handler.HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(657, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldIncreasePremiumByTwentyPercentIfYoungestDriverBetween21And25OnStartDate()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-23)
                    },
                    new InsurancePolicyDriver
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-64)
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationYoungestDriverHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(120, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldDecreasePremiumByTenPercentIfYoungestDriverBetween26And75OnStartDate()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-30)
                    },
                    new InsurancePolicyDriver
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-64)
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationYoungestDriverHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(90, returnedPremium);
        }
    }
}
