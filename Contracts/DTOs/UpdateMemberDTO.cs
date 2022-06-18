using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class UpdateMemberDTO
    {
        public string Id { get; set; }
        [MaxLength(30, ErrorMessage = "Max characters are 30.")]
        [MinLength(3, ErrorMessage = "Min Characters are 3.")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "Max characters are 30.")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public float Hours { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        [MinLength(3, ErrorMessage = "min password lenght is 3")]
        public string Password { get; set; }
    }
}
