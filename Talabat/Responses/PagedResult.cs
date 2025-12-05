namespace Talabat.API.Responses
{
    public class PagedResult<T>
    {
        public IEnumerable<T>? Items { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // the returned data from the query not the whole database expect the user didn't filtered or searched
        public bool HasNextPage => (PageIndex + 1) * PageSize < Count;
        public bool HasPreviousPage => PageIndex > 1;

        public PagedResult(int _pageIndex, int _pageSize, int _count)
        {
            PageIndex = _pageIndex;
            PageSize = _pageSize;
            Count = _count;
        }
    }

}
