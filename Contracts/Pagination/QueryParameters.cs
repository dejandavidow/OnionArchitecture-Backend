namespace Contracts.Pagination
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
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
        public string Search { get; set; }
        public string Letter { get; set; }
        public string OrderBy { get; set; }
    }
}
