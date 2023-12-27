using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
