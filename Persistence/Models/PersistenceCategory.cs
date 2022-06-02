using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class PersistenceCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(20,ErrorMessage ="Max characters are 20.")]
        [MinLength(3,ErrorMessage ="Min characters are 3.")]
        public string Name { get; set; }
    }
}