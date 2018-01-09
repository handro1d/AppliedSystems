using System;
using System.ComponentModel.DataAnnotations;

namespace AppliedSystems.Domain
{
    public abstract class EntityBase
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        protected EntityBase()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
