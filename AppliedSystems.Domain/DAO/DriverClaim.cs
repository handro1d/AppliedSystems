using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppliedSystems.Domain.DAO
{
    public sealed class DriverClaim : EntityBase, IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [Required]
        public DateTime DateOfClaim { get; set; }

        // Navigation Properties
        public InsurancePolicyDriver Driver { get; set; }
    }
}
