using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Persistence.Models
{
    public class PersistenceTimeSheet
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(5)]
        public int Time{get;set;}
        [MaxLength(5)]
        public int OverTime{get;set;}
        public DateTime Date{get;set;}
        
        public Guid? ClientId{get;set;}
        [ForeignKey(nameof(ClientId))]
        public PersistenceClient Client{get;set;}
        public Guid? ProjectId{get;set;}
        [ForeignKey(nameof(ProjectId))]
        public PersistenceProject Project{get;set;}
        public Guid? CategoryId{get;set;}
        [ForeignKey(nameof(CategoryId))]
        public PersistenceCategory Category{get;set;}
    }
}