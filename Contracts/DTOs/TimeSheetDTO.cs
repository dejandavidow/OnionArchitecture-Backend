using System;

namespace Contracts.DTOs
{
    public class TimeSheetDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Time { get; set; }

        public decimal Overtime { get; set; }
        public DateTime Date { get; set; }

        public string ClientName { get; set; }

        public string ProjectName { get; set; }

        public string CategoryName { get; set; }
    }
}
