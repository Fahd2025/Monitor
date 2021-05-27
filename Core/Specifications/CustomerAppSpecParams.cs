namespace Core.Specifications
{
    public class CustomerAppSpecParams
    {
        
        private const int MaxPageSize = 500;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 100;

        public int PageSize { 
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize ? MaxPageSize : value);
        }

        public int? AppInfoId { get; set; }
        public int? CustomerId { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search; 
            set => _search = value.ToLower(); 
        }
        

    }
}