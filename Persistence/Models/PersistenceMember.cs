using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Persistence.Models
{
    public class PersistenceMember
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username{get;set;}
        public string Email { get; set; }
        public float Hours { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ResetToken { get; set; } = null;
    }
}