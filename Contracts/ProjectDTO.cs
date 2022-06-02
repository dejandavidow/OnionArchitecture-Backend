
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class ProjectDTO
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Max characters are 30.")]
        [MinLength(3, ErrorMessage = "Min characters are 3.")]
        public string ProjectName { get; set; }
        [MaxLength(500, ErrorMessage = "Max characters are 500.")]
        public string Description { get; set; } = string.Empty;
        public bool Archive { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage ="This Field is required.")]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string MemberId{get;set;}
    }
}