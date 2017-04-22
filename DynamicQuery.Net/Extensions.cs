using System.Linq;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Services;

namespace DynamicQuery.Net
{
    public static class Extensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> input , FilterInput[] filterInputs)
        {
            return FilterService.Filter(input, filterInputs);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, FilterInput filterinput)
        {
            return FilterService.Filter(input, filterinput);
        }
    }
}
