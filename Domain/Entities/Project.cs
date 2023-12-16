using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public bool Archive { get; set; }
        [Required]
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string UserId { get; set; }
    }
}
