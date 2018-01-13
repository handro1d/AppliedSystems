using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Domain.DAO
{
    public sealed class User : EntityBase, IEntity, IUser<int>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string SecurityStamp { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Navigation Properties
        public ICollection<Role> Roles { get; set; }

        public User()
        {
            Roles = new HashSet<Role>();
        }
    }
}
