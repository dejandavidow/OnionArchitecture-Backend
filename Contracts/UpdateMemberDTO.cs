using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class UpdateMemberDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
        public float Hours { get; set; }
        public string Status { get; set; }
        public string Role { get; set; } 

        public string Password { get; set; }
    }
}
