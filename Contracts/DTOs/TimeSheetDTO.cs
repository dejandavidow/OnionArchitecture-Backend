using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs
{
    public class TimeSheetDTO
    {
        public string Id { get; set; }
        [MaxLength(500, ErrorMessage = "Max characters are 500.")]
        public string Description { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]

        public float Time { get; set; }

        public float OverTime { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string ProjectId { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string CategoryId { get; set; }
    }
}
