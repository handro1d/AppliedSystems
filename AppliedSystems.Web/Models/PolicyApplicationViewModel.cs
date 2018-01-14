using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using Newtonsoft.Json;

namespace AppliedSystems.Web.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class PolicyApplicationViewModel
    {
        public List<SelectListItem> Occupations { get; private set; }

        public List<PolicyApplicationDriverViewModel> Drivers { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Policy Start Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public PolicyApplicationViewModel()
        {
            Occupations = new List<SelectListItem>();
            Drivers = new List<PolicyApplicationDriverViewModel>();
        }

        public PolicyApplicationViewModel(IReferenceService referenceService)
            : this()
        {
            if (referenceService == null)
            {
                throw new ArgumentNullException("referenceService");
            }

            PopulateAvailableOptions(referenceService);
        }

        public void PopulateAvailableOptions(IReferenceService referenceService)
        {
            Occupations = referenceService.Get<RefOccupation>()
                .Select(o => new SelectListItem {Text = o.Description, Value = o.Id.ToString()}).ToList();
        }
    }

    public sealed class PolicyApplicationDriverViewModel
    {
        public List<PolicyApplicationDriverClaimViewModel> Claims { get; set; }

        [MaxLength(100, ErrorMessage = "First Name must be less than 100 characters")]
        [Required(ErrorMessage = "First Name is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Surname must be less than 100 characters")]
        [Required(ErrorMessage = "Surname is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "surname", Required = Required.Always)]
        public string Surname { get; set; }

        [DisplayName("Occupation")]
        [Range(0, byte.MaxValue, ErrorMessage = "Occupation is required")]
        [JsonProperty(PropertyName = "occupationId", Required = Required.Always)]
        public byte OccupationId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        public PolicyApplicationDriverViewModel()
        {
            Claims = new List<PolicyApplicationDriverClaimViewModel>();

        }
    }

    public sealed class PolicyApplicationDriverClaimViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Date of Claim is required")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfClaim { get; set; }
    }
}