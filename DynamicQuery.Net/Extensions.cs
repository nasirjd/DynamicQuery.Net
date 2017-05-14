using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Services;

namespace DynamicQuery.Net
{
    public static class Extensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> input , List<FilterInput> filterInputs)
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

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, DynamicQueryNetInput dynamicInput)
        {
            if (dynamicInput == null) return (IOrderedQueryable<T>) input;

            input = input.Filter(dynamicInput.Filter);

            if (dynamicInput.Paging != null)
            {
                if (dynamicInput.Order != null)
                {
                    input = input.Order(dynamicInput.Order)
                        .Skip(dynamicInput.Paging.Page 
                              * dynamicInput.Paging.Size).Take(dynamicInput.Paging.Size);
                }
            }
            else
            {
                input = input.Order(dynamicInput.Order);
            }
            
            return (IOrderedQueryable<T>) input;

        }


        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput orderInput)
        {
            return orderInput !=null ? OrderService.Ordering(input , orderInput) : (IOrderedQueryable<T>)input;
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, List<OrderInput> orderInput)
        {
            return orderInput !=null ? OrderService.Ordering(input , orderInput) : (IOrderedQueryable<T>)input;
        }

    }
}
