using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client

    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Adress { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
    }
}
