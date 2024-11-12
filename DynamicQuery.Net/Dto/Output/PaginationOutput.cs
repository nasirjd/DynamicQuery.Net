using System.Linq;

namespace DynamicQuery.Net.Dto.Output
{
    public class PaginationOutput<T>
    {
        public IOrderedQueryable<T> Data { get; set; }
        public int Count { get; set; }
    }
}