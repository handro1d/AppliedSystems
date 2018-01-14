using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AppliedSystems.Common;

namespace AppliedSystems.Domain.DAO
{
    public class InsurancePolicy : EntityBase, IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Premium { get; set; }

        // Navigation properties
        public User User { get; set; }
        public virtual ICollection<InsurancePolicyDriver> Drivers { get; set; }

        // Additional properties
        [NotMapped]
        public InsurancePolicyDriver YoungestDriver
        {
            get
            {
                if (Drivers == null || !Drivers.Any())
                {
                    return null;
                }

                return Drivers.OrderByDescending(d => d.DateOfBirth).FirstOrDefault();
            }
        }

        [NotMapped]
        public InsurancePolicyDriver OldestDriver
        {
            get
            {
                if (Drivers == null || !Drivers.Any())
                {
                    return null;
                }

                return Drivers.OrderBy(d => d.DateOfBirth).FirstOrDefault();
            }
        }

        public InsurancePolicy()
        {
            Drivers = new HashSet<InsurancePolicyDriver>();
        }

        public PolicyValidationResult Validate()
        {
            if (StartDate < DateTime.UtcNow)
            {
                return new PolicyValidationResult("Start Date of Policy");
            }

            if (!Drivers.Any())
            {
                return new PolicyValidationResult("A minimum of 1 driver must be added to a policy");
            }

            if (Drivers.Count > 5)
            {
                return new PolicyValidationResult("A maximum of 5 drivers can be added to a policy");
            }

            if (Drivers.Any(d => d.DateOfBirth > DateTime.UtcNow))
            {
                return new PolicyValidationResult("Date of Birth for all drivers must not be in the future");
            }

            if (Drivers.SelectMany(d => d.Claims).Any(c => c.DateOfClaim > DateTime.UtcNow))
            {
                return new PolicyValidationResult("Claims must not have a date in the future");
            }

            var youngestDriver = YoungestDriver;
            var ageOfYoungestDriver = youngestDriver.DateOfBirth.CalculateAgeOnDate(StartDate);
            if (ageOfYoungestDriver < 21)
            {
                return new PolicyValidationResult(string.Format("Age of Youngest Driver: {0}", youngestDriver.Name));
            }

            var oldestDriver = OldestDriver;
            var ageOfOldestDriver = oldestDriver.DateOfBirth.CalculateAgeOnDate(StartDate);
            if (ageOfOldestDriver > 75)
            {
                return new PolicyValidationResult(string.Format("Age of Oldest Driver: {0}", oldestDriver.Name));
            }

            var driverWithMoreThan2Claims = Drivers.FirstOrDefault(d => d.Claims.Count > 2);
            if (driverWithMoreThan2Claims != null)
            {
                return new PolicyValidationResult(string.Format("Driver has more than 2 claims: {0}", driverWithMoreThan2Claims.Name));
            }

            var totalClaims = Drivers.Sum(d => d.Claims.Count);
            if (totalClaims > 3)
            {
                return new PolicyValidationResult("Policy has more than 3 claims");
            }

            return new PolicyValidationResult(PolicyValidResult.Succeeded);
        }
    }
}
