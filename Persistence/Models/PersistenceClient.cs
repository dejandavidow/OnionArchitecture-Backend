using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models{
    public class PersistenceClient {
        [Key]
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country {get;set;}
    }
}