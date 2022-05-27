using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence.Models
{
public class PersistenceProject
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string ProjectName { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public bool Archive { get; set; }
    public bool Status {get;set;}
    public Guid? ClientId{get;set;}
    [ForeignKey(nameof(ClientId))]
    public PersistenceClient Client { get; set; }
    public Guid? MemberId{get;set;}
    [ForeignKey(nameof(MemberId))]
    public PersistenceMember Member {get;set;}
}
}