using System;
using System.Collections.Generic;
using AppliedSystems.Common;
using AppliedSystems.Domain;
using AppliedSystems.Domain.DAO;
using NUnit.Framework;

namespace AppliedSystems.Services.Tests
{
    [TestFixture]
    internal sealed class InsurancePremiumCalculationOccupationHandlerFixture
    {
        [Test]
        public void HandleRequest_ShouldReturnPremiumIfNoDriversWithSearchedForApplications()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        Occupation = new RefOccupation
                        {
                            Code = "ANOTHER_JOB"
                        }
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationOccupationHandler()
                .HandleRequest(policy, 789);

            // Verify result
            Assert.AreEqual((double)789, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldCallSuccessor()
        {
            // Setup dependencies
            var policy = new InsurancePolicy();

            var testHandler = new TestHandler()
                .WithReturnValue(456);

            var handler = new InsurancePremiumCalculationOccupationHandler();
            handler.SetSuccessor(testHandler);

            // Call HandleRequest
            var returnedPremium = handler.HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual(456, returnedPremium);
        }

        [Test]
        public void HandleRequest_ShouldIncreasePremiumByTenPercentIfAnyChauffeurs()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        Occupation = new RefOccupation
                        {
                            Code = Occupation.Chauffeur.Description()
                        }
                    },
                    new InsurancePolicyDriver
                    {
                        Occupation = new RefOccupation
                        {
                            Code = Occupation.Chauffeur.Description()
                        }
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationOccupationHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual((double)110, Math.Round(returnedPremium, 2));
        }

        [Test]
        public void HandleRequest_ShouldDecreasePremiumByTenPercentIfAnyAccountants()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        Occupation = new RefOccupation
                        {
                            Code = Occupation.Accountant.Description()
                        }
                    },
                    new InsurancePolicyDriver
                    {
                        Occupation = new RefOccupation
                        {
                            Code = Occupation.Accountant.Description()
                        }
                    }
                }
            };

            // Call HandleRequest
            var returnedPremium = new InsurancePremiumCalculationOccupationHandler()
                .HandleRequest(policy, 100);

            // Verify result
            Assert.AreEqual((double)90, Math.Round(returnedPremium, 2));
        }
    }
}
