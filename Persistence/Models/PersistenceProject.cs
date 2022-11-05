using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence.Models
{
public class PersistenceProject
{
        [Key]
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Archive { get; set; }
        public string Status {get;set;}
        public Guid? ClientId{get;set;}
        [ForeignKey(nameof(ClientId))]
        public PersistenceClient Client { get; set; }
        public Guid? MemberId{get;set;}
        [ForeignKey(nameof(MemberId))]
        public PersistenceMember Member {get;set;}
}
}