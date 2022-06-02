using System;
namespace Domain
{
    public class Member
    {
        public Member(Guid id,string Name,string Username,string Email,float Hours,bool Status,bool Role)
        {
            this.Id=id;
            this.Name=Name;
            this.Username=Username;
            this.Email=Email;
            this.Hours=Hours;
            this.Status=Status;
            this.Role=Role;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Username{get; private set;}
        public string Email { get; private set; }
        public float Hours { get; private set; }
        public bool Status { get; private set; }
        public bool Role { get; private set; }
        public Member UpdateName(string name)
        {
            return new Member(this.Id,name ?? this.Name,this.Username,this.Email,this.Hours,this.Status,this.Role);
        }
        public Member UpdateUserName(string username)
        {
            return new Member(this.Id,this.Name,username ?? this.Username,this.Email,this.Hours,this.Status,this.Role);
        } 
        public Member UpdateEmail(string email)
        {
            return new Member(this.Id,this.Name,this.Username,email ?? this.Email,this.Hours,this.Status,this.Role);
        }
        public Member UpdateHours(float hours)
        {
            return new Member(this.Id,this.Name,this.Username,this.Email, hours >0 ? hours : this.Hours , this.Status,this.Role);
        }
        public Member UpdateStatus(bool status)
        {
            return new Member(this.Id, this.Name, this.Username, this.Email, this.Hours,status == true ? status : this.Status,this.Role);
        } 
        public Member UpdateRole(bool role)
        {
            return new Member(this.Id,this.Name,this.Username,this.Email,this.Hours,this.Status, role == true ? role : this.Role);
        }         
    }
}
