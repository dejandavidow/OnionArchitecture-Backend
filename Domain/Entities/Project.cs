using System;
namespace Domain
{
    public class Project
    {
        public Project(Guid Id,string ProjectName,string Description,bool Archive,bool Status,Client client,Member member)
        {
            this.Id = Id;
            this.ProjectName= ProjectName;
            this.Description = Description;
            this.Archive = Archive;
            this.Status = Status;
            this.Client = client;
            this.Member = member;
        }
        public Guid Id { get; private set; }
        public string ProjectName { get; private set; }
        public string Description { get; private set; }
        public bool Archive { get; private set; }
        public bool Status {get; private set;}
        public Client Client { get; private set; }
        public Member Member {get; private set;}
        public Project UpdatePname(string name)
        {
            return new Project(this.Id, name ?? this.ProjectName,this.Description,this.Archive,this.Status,this.Client, this.Member);
        }
         public Project UpdateDescription(string description)
        {
            return new Project(this.Id, this.ProjectName,description ?? this.Description,this.Archive,this.Status,this.Client, this.Member);
        }
         public Project UpdateArchive(bool archive)
        {
            return new Project(this.Id,this.ProjectName,this.Description, archive == true ? archive : this.Archive,this.Status,this.Client, this.Member);
        }
         public Project UpdateStatus(bool status)
        {
            return new Project(this.Id,this.ProjectName,this.Description,this.Archive, status == true ? status : this.Status ,this.Client, this.Member);
        }
         public Project UpdateClient(Client client)
        {
            return new Project(this.Id,this.ProjectName,this.Description,this.Archive,this.Status,client ?? this.Client, this.Member);
        }
         public Project UpdateMember(Member member)
        {
            return new Project(this.Id, this.ProjectName,this.Description,this.Archive,this.Status,this.Client, member ?? this.Member);
        }
        
    }
}
