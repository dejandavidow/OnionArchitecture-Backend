using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs
{
    public class TimeSheetDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public float Time { get; set; }

        public float OverTime { get; set; }
        public string Date { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
        public ProjectDTO ProjectDTO { get; set; }
    }
}
