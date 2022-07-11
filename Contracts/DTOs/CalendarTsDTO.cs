using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CalendarTsDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public float Time { get; set; }

        public float OverTime { get; set; }
        public DateTime Date { get; set; }
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CategoryId { get; set; }
    }
}
