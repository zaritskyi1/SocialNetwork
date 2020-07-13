namespace SocialNetwork.BLL.Helpers
{
    public class PaginationQuery
    {
        private const int MaxPageSize = 50;
        private const int MinPageSize = 10;

        public int PageNumber 
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                _pageNumber = value > 1? value : _pageNumber;
            }
        }

        private int _pageSize = 10;
        private int _pageNumber = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > MaxPageSize || value < MinPageSize) ? MaxPageSize : value;
            }
        }
    }
}
