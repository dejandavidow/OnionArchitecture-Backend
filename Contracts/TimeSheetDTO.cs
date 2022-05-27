using System;
namespace Contracts
{
    public class TimeSheetDTO
    {
      public string Id { get; set; }
        public string Description { get; set; }
        public int Time{get;set;}
        public int OverTime{get;set;}
        public DateTime Date{get;set;}
        public string ClientId{get;set;}
        public string ProjectId{get;set;}
        public string CategoryId{get;set;}
    }
}
