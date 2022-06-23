using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class PostProjectDTO
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Max characters are 30.")]
        [MinLength(3, ErrorMessage = "Min characters are 3.")]
        public string ProjectName { get; set; }
        [MaxLength(500, ErrorMessage = "Max characters are 500.")]
        public string Description { get; set; } = string.Empty;
        public string Archive { get; set; } = "No-Archive";
        public string Status { get; set; } = "active";
        [Required(ErrorMessage = "This Field is required.")]

        public string ClientId { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string MemberId { get; set; }
    }
}
