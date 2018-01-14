using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppliedSystems.Domain.DAO
{
    public class InsurancePolicyDriver : EntityBase, IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("InsurancePolicy")]
        public int InsurancePolicyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [ForeignKey("Occupation")]
        public byte OccupationId { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // Navigation Properties
        public InsurancePolicy InsurancePolicy { get; set; }
        public virtual RefOccupation Occupation { get; set; }
        public ICollection<DriverClaim> Claims { get; set; }

        // Additional Properties
        [NotMapped]
        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, Surname); }
        }

        public InsurancePolicyDriver()
        {
            Claims = new HashSet<DriverClaim>();
        }
    }
}
