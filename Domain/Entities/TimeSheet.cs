using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class TimeSheet
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public decimal Time { get; set; }
        [Required]
        public decimal OverTime { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
