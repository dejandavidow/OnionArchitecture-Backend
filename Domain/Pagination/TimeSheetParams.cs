using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagination
{
    public class TimeSheetParams
    {
        public DateTime? FilterStart { get; set; }
        public DateTime? FilterEnd { get; set; }
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string CategoryId { get; set; }
        public string MemberId { get; set; }
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
