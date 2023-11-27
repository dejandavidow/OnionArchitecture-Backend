namespace Contracts.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Archive { get; set; }
        public bool Status { get; set; }
        public string ClientName { get; set; }
        public string UserId { get; set; }
    }
}