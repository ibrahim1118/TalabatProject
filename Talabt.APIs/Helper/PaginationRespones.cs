using Talabt.APIs.Dtos;

namespace Talabt.APIs.Helper
{
    public class PaginationRespones<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }   

        public int TotalCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public PaginationRespones(int pageIndex , int pageSize , int totlaCount , IReadOnlyList<T> data)
        {
            PageIndex= pageIndex;
            PageSize = pageSize;
            TotalCount= totlaCount;
            Data = data;
        }

    }
}
