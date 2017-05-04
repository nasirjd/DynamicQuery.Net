using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, OrderFilterInput orderFilterInput)
        {
            var result = input.Filter(orderFilterInput.Filter);
                return orderFilterInput.Order == null ? (IOrderedQueryable<T>)result : result.Order(orderFilterInput.Order);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput orderInput)
        {
            return OrderService.Ordering(input , orderInput);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput[] orderInput)
        {
            return OrderService.Ordering(input , orderInput);
        }

    }
}
