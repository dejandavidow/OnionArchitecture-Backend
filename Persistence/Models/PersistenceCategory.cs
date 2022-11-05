using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class PersistenceCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}