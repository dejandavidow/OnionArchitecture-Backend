using System;
namespace Domain.Entities
{
    public class TimeSheet
    {
        public TimeSheet(Guid id,string Description, float Time, float OverTime,DateTime Date, Client client,Project project,Category category)
        {
            this.Id = id;
            this.Description = Description;
            this.Time = Time;
            this.OverTime = OverTime;
            this.Date = Date;
            this.Client = client;
            this.Project = project;
            this.Category = category;
        }
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public float Time{get;private set;}
        public float OverTime {get;private set;}
        public DateTime Date { get;private set;}
        public Client Client{get; private set;}
        public Project Project{get; private set;}
        public Category Category{get;private set;}

        public TimeSheet UpdateDescription(string description)
        {
            return new TimeSheet(this.Id,description ?? this.Description,this.Time,this.OverTime,this.Date,this.Client,this.Project,this.Category);
        }
         public TimeSheet UpdateTime(float time)
        {
            return new TimeSheet(this.Id,this.Description,time >0 ? time : this.Time,this.OverTime,this.Date, this.Client,this.Project,this.Category);
        }
        public TimeSheet UpdateOvertime(float overtime)
        {
            return new TimeSheet(this.Id,this.Description,this.Time,overtime >0 ? overtime : this.OverTime,this.Date, this.Client,this.Project,this.Category);
        }
        public TimeSheet ClientUpdate(Client Client)
        {
            return new TimeSheet(this.Id, this.Description, this.Time, this.OverTime, this.Date,Client ?? this.Client, this.Project, this.Category);
        }
        public TimeSheet ProjectUpdate(Project Project)
        {
            return new TimeSheet(this.Id, this.Description, this.Time,this.OverTime, this.Date, this.Client,Project ?? this.Project, this.Category);
        }
        public TimeSheet CategoryUpdate(Category Category)
        {
            return new TimeSheet(this.Id, this.Description, this.Time, this.OverTime, this.Date, this.Client, this.Project, Category ?? this.Category);
        }

    }
}
