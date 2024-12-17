using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
