using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs
{
    public class TimeSheetDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public float Time { get; set; }
        [Required]
        public float Overtime { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]

        public int ClientId { get; set; }
        [Required]

        public int ProjectId { get; set; }
        [Required]

        public int CategoryId { get; set; }
    }
}
