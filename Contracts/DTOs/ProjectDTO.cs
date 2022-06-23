using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs
{
    public class ProjectDTO
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Archive { get; set; }
        public string Status { get; set; }
        public ClientDTO ClientDTO { get; set; }
        public GetMemberDTO MemberDTO { get; set; }
      
    }
}