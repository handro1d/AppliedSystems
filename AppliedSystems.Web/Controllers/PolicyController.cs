using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppliedSystems.Common;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using AppliedSystems.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AppliedSystems.Web.Controllers
{
    [RoutePrefix("Policy")]
    public class PolicyController : Controller
    {
        private readonly IReferenceService _referenceService;
        private readonly IPolicyService _policyService;
        private readonly IAuthenticationManager _authenticationManager;

        public PolicyController(
            IReferenceService referenceService,
            IPolicyService policyService,
            IAuthenticationManager authenticationManager)
        {
            if (referenceService == null)
            {
                throw new ArgumentNullException("referenceService");
            }

            if (policyService == null)
            {
                throw new ArgumentNullException("policyService");
            }

            if (authenticationManager == null)
            {
                throw new ArgumentNullException("authenticationManager");
            }

            _referenceService = referenceService;
            _policyService = policyService;
            _authenticationManager = authenticationManager;
        }

        [Route("Apply")]
        [HttpGet]
        public ActionResult Apply()
        {
            var viewModel = new PolicyApplicationViewModel(_referenceService);

            return View("Apply", viewModel);
        }

        [Route("Apply")]
        [HttpPost]
        public ActionResult Apply(PolicyApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult(ModelState);
            }

            var newPolicy = new InsurancePolicy
            {
                UserId = Convert.ToInt32(_authenticationManager.User.Identity.GetUserId()),
                ApplicationDate = DateTime.UtcNow,
                StartDate = model.StartDate,
                Drivers = model.Drivers.Select(d => new InsurancePolicyDriver
                {
                    DateOfBirth = d.DateOfBirth,
                    FirstName = d.FirstName.Trim(),
                    Surname = d.Surname.Trim(),
                    OccupationId = d.OccupationId,
                    Claims = d.Claims.Select(c => new DriverClaim
                    {
                        DateOfClaim = c.DateOfClaim
                    }).ToList()
                }).ToList()
            };

            var policyValidationResult = newPolicy.Validate();

            if (!policyValidationResult.Succeeded)
            {
                return new BadRequestResult(policyValidationResult.Error);
            }

            TransactionHelper.InvokeTransaction(() =>
            {
                newPolicy = _policyService.AddInsurancePolicyApplication(newPolicy);
                newPolicy.Premium = _policyService.CalculatePremium(newPolicy, 500);
                _policyService.UpdateInsurancePolicy(newPolicy);
            });

            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }
    }
}
 