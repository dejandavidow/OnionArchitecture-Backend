using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class PersistenceMember
    {
      [Key]
      public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Username{get;set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(5)]
        public int Hours { get; set; }
        public bool Status { get; set; }
        public bool Role { get; set; }
    }
}