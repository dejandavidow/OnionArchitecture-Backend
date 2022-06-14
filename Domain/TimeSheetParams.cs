using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TimeSheetParams
    {
        public DateTime FilterStart { get; set; }
        public DateTime FilterEnd { get; set; }
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CategoryId { get; set; }
    }
}
