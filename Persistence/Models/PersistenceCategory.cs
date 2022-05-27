using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class PersistenceCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}