namespace Ecom.core.Sharing
{
    public class ProductParams
    {
        public string Sort { get; set; }
        public int CategoryId { get; set; }
        public int MaxPageSize { get; set; } = 6;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 3;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = _pageSize > MaxPageSize ? MaxPageSize : value; }
        }


    }
}
