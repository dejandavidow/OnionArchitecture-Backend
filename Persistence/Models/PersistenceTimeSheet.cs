using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Persistence.Models
{
    public class PersistenceTimeSheet
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public float Time {get;set;}
        public float OverTime {get;set;}
        public DateTime Date { get;set; }
        
        public Guid? ClientId{get;set;}
        [ForeignKey(nameof(ClientId))]
        [Required]
        public PersistenceClient Client{get;set;}
        public Guid? ProjectId{get;set;}
        [ForeignKey(nameof(ProjectId))]
        [Required]
        public PersistenceProject Project{get;set;}
        public Guid? CategoryId{get;set;}
        [ForeignKey(nameof(CategoryId))]
        [Required]
        public PersistenceCategory Category{get;set;}
    }
}