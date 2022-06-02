using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence.Models
{
public class PersistenceProject
{
        [Key]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Max characters are 30.")]
        [MinLength(3, ErrorMessage = "Min characters are 3.")]
        public string ProjectName { get; set; }
        [MaxLength(500,ErrorMessage ="Max characters are 500.")]
        public string Description { get; set; } = string.Empty;
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