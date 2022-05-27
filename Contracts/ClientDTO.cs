using System.ComponentModel.DataAnnotations;
namespace Contracts
{
    public class ClientDTO
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string ClientName { get;set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Adress { get;set; }
        [MaxLength(20)]
        public string City { get;set;}
        public string PostalCode{get;set;}
        public string Country {get;set;}
    }
}