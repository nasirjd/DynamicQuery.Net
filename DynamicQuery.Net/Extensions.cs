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
            return filterInputs != null ? FilterService.Filter(input, filterInputs) : input;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, FilterInput filterinput)
        {
            return filterinput !=null ? FilterService.Filter(input, filterinput) : input;
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, OrderFilterInput orderFilterInput)
        {
            return (IOrderedQueryable<T>) (orderFilterInput != null ?
                input.Filter(orderFilterInput.Filter).Order(orderFilterInput.Order) : input);
        }
        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, OrderFilterNonFilterInput orderFilterInput)
        {
            return (IOrderedQueryable<T>) (orderFilterInput != null ?
                input.Filter(orderFilterInput.Filter).Order(orderFilterInput.Order) : input);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput orderInput)
        {
            return orderInput !=null ? OrderService.Ordering(input , orderInput) : (IOrderedQueryable<T>)input;
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput[] orderInput)
        {
            return orderInput !=null ? OrderService.Ordering(input , orderInput) : (IOrderedQueryable<T>)input;
        }

    }
}
