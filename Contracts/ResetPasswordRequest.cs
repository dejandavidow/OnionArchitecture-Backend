using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ResetPasswordRequest
    {
        //[Required(ErrorMessage ="Token is required.")]
       // public string Token { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(8,ErrorMessage ="Password must have atleast 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirm password is required.")]
        [Compare("Password",ErrorMessage ="Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
