using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ChangePassword
{
    public class ChangePasswordRequest
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Old password is required.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8,ErrorMessage ="Min password lenght:8")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="Confirm password is required.")]
        [Compare("NewPassword",ErrorMessage ="Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
