using Talabat.DTO;

namespace Talabat.Helper
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
  

        public Pagination(int pageIndex,int count ,int pageSiza, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSiza;
            Data = data;
            
            Count = count;

        }
    }
}
