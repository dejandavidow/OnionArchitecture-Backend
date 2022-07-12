using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}
