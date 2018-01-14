using System;
using System.Collections.Generic;
using AppliedSystems.Domain.DAO;
using NUnit.Framework;

namespace AppliedSystems.Domain.Tests
{
    [TestFixture]
    internal sealed class InsurancePolicyFixture
    {
        [Test]
        public void ValidatePolicy_ShouldFailIfStartDateInThePast()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow.AddDays(-3)
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Start Date of Policy", result.Error);
        }

        [Test]
        public void ValidatePolicy_ShouldFailIfYoungestDriverLessThan21()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver",
                        DateOfBirth = DateTime.UtcNow.AddYears(-17)
                    }
                }
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Age of Youngest Driver: Test Driver", result.Error);
        }

        [Test]
        public void ValidatePolicy_ShouldFailIfOldestDriverGeaterThan75()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver",
                        DateOfBirth = DateTime.UtcNow.AddYears(-77)
                    }
                }
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Age of Oldest Driver: Test Driver", result.Error);
        }

        [Test]
        public void ValidatePolicy_ShouldFailIfMoreThan2ClaimsForDriver()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow.AddMonths(1),
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver",
                        DateOfBirth = DateTime.UtcNow.AddYears(-30),
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim(),
                            new DriverClaim(),
                            new DriverClaim(),
                            new DriverClaim()
                        }
                    }
                }
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Driver has more than 2 claims: Test Driver", result.Error);
        }

        [Test]
        public void ValidatePolicy_ShouldFailIfMoreThan3Claims()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow.AddMonths(1),
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver1",
                        DateOfBirth = DateTime.UtcNow.AddYears(-30),
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim(),
                            new DriverClaim(),
                        }
                    },
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver2",
                        DateOfBirth = DateTime.UtcNow.AddYears(-30),
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim(),
                            new DriverClaim(),
                        }
                    }
                }
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Policy has more than 3 claims", result.Error);
        }

        [Test]
        public void ValidatePolicy_ShouldSucceed()
        {
            // Setup dependencies
            var policy = new InsurancePolicy
            {
                StartDate = DateTime.UtcNow,
                Drivers = new List<InsurancePolicyDriver>
                {
                    new InsurancePolicyDriver
                    {
                        FirstName = "Test",
                        Surname = "Driver",
                        DateOfBirth = DateTime.UtcNow.AddYears(-30),
                        Claims = new List<DriverClaim>
                        {
                            new DriverClaim(),
                        }
                    }
                }
            };

            // Call Validate
            var result = policy.Validate();

            // Verify result
            Assert.IsTrue(result.Succeeded);
        }
    }
}