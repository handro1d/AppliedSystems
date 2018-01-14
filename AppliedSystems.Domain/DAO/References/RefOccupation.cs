using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppliedSystems.Domain.DAO
{
    public sealed class RefOccupation : IReferenceEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(10)]
        [Index("IX_OccupationCode", 1, IsUnique = true)]
        public string Code { get; set; }
    }
}
