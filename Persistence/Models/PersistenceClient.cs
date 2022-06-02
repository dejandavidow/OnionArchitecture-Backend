using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models{
    public class PersistenceClient {
        [Key]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(30,ErrorMessage ="Max characters are 30.")]
        [MinLength(3,ErrorMessage ="Min Characters are 3.")]
        public string ClientName { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country {get;set;} = string.Empty;
    }
}