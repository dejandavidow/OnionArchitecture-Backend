
namespace Contracts
{
    public class ProjectDTO
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public bool Archive { get; set; }
        public bool Status {get;set;}
        public ClientDTO ClientDTO { get; set; }
        public MemberDTO MemberDTO {get;set;}
    }
}