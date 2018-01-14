using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Tests.Common;
using AppliedSystems.Web.Models;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;

namespace AppliedSystems.Web.Tests
{
    [TestFixture]
    internal sealed class PolicyControllerFixture
    {
        private IAuthenticationManager _authenticationManager;
        private PolicyControllerBuilder _controller;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var authenticatedIdentity = new MockIdentity(true);
            var authenticatedUser = new ClaimsPrincipal(authenticatedIdentity);
            authenticatedIdentity.AddClaim(new Claim("ID", "123"));

            _authenticationManager = new FakeAuthenticationManager()
                .WithUser(authenticatedUser).Build().Object;
        }

        [SetUp]
        public void SetUp()
        {
            _controller = new PolicyControllerBuilder()
                .WithAuthenticationManager(_authenticationManager);
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfReferenceServiceIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _controller.WithReferenceService(null).Build();
            }, "referenceService");
        }

        [Test]
        public void Ctor_ShouldThrowExceptionIfPolicyServiceIsNull()
        {
            CustomAssertions.AssertArgumentNullExceptionThrown(() =>
            {
                _controller.WithPolicyService(null).Build();
            }, "policyService");
        }

        [Test]
        public void Apply_ShouldReturnApplicationView()
        {
            // Call Apply
            var result = _controller.Build().Apply();

            // Verify result
            result.AssertIsView("Apply");
        }

        [Test]
        public void Apply_ShouldReturnErrorIfInvalidModel()
        {
            // Setup dependencies
            var controller = _controller.Build();
            var viewModel = new PolicyApplicationViewModel();
            controller.ModelState.AddModelError("Test Error", @"Test Error Message");

            // Call Apply
            var result = controller.Apply(viewModel);
            
            // Verify result
            result.AssertIsBadRequestResultStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Apply_ShouldReturnErrorIfDOBInFuture()
        {
            // Setup dependencies
            var controller = _controller.Build();

            var viewModel = new PolicyApplicationViewModel
            {
                StartDate = DateTime.UtcNow.AddDays(14),
                Drivers = new List<PolicyApplicationDriverViewModel>
                {
                    new PolicyApplicationDriverViewModel
                    {
                        FirstName = "Test",
                        Surname = "User",
                        DateOfBirth = DateTime.UtcNow.AddDays(1),
                        Claims = new List<PolicyApplicationDriverClaimViewModel>()
                    }
                }
            };

            // Call Apply
            var result = controller.Apply(viewModel);

            // Verify result
            result.AssertIsStatus(HttpStatusCode.BadRequest, "Date of Birth for all drivers must not be in the future");
        }

        [Test]
        public void Apply_ShouldReturnErrorIfClaimDateInFuture()
        {
            // Setup dependencies
            var controller = _controller.Build();

            var viewModel = new PolicyApplicationViewModel
            {
                StartDate = DateTime.UtcNow.AddDays(27),
                Drivers = new List<PolicyApplicationDriverViewModel>
                {
                    new PolicyApplicationDriverViewModel
                    {
                        FirstName = "Test",
                        Surname = "User",
                        DateOfBirth = DateTime.UtcNow.AddYears(-32),
                        Claims = new List<PolicyApplicationDriverClaimViewModel>
                        {
                            new PolicyApplicationDriverClaimViewModel{ DateOfClaim = DateTime.UtcNow.AddDays(1) }
                        }
                    }
                }
            };

            // Call Apply
            var result = controller.Apply(viewModel);

            // Verify result
            result.AssertIsStatus(HttpStatusCode.BadRequest, "Claims must not have a date in the future");
        }

        [Test]
        public void Apply_ShouldAddPolicy()
        {
            // Setup dependencies
            var policyService = new FakePolicyService().Build();
            var startDate = DateTime.UtcNow.AddDays(14);
            var claimDate = DateTime.UtcNow.AddDays(-20);

            var request = new PolicyApplicationViewModel
            {
                StartDate = startDate,
                Drivers = new List<PolicyApplicationDriverViewModel>
                {
                    new PolicyApplicationDriverViewModel
                    {
                        DateOfBirth = DateTime.UtcNow.AddYears(-27),
                        FirstName = "Test",
                        Surname = "Driver",
                        OccupationId = 123,
                        Claims = new List<PolicyApplicationDriverClaimViewModel>
                        {
                            new PolicyApplicationDriverClaimViewModel
                            {
                                DateOfClaim = claimDate
                            }
                        }
                    }
                }
            };

            // Call Apply
            _controller
                .WithPolicyService(policyService.Object).Build()
                .Apply(request);

            // Verify result
            policyService.Verify(
                p => p.AddInsurancePolicyApplication(It.Is<InsurancePolicy>(ip =>
                    ip.StartDate == startDate && ip.Drivers.Count == 1)), Times.Once);
        }
    }
}
