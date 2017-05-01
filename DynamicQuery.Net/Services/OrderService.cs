using System.Linq;
using System.Linq.Expressions;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Utility;

namespace DynamicQuery.Net.Services
{
    public class OrderService
    {
        public static IOrderedQueryable<T> Ordering<T>(IQueryable<T> input, OrderInput[] orderInputs)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var firstOrderInput = orderInputs[0];
            var result = firstOrderInput.Order == OrderTypeEnum.Asc
                ? OrderingHelper.OrderBy(input, firstOrderInput.Property, parameter)
                : OrderingHelper.OrderByDescending(input, firstOrderInput.Property, parameter);

            for (int i = 1; i < orderInputs.Length; i++)
            {
                var orderInput = orderInputs[i];
                result = orderInput.Order == OrderTypeEnum.Asc
                    ? OrderingHelper.ThenBy(result, orderInput.Property, parameter)
                    : OrderingHelper.ThenByDescending(result, orderInput.Property, parameter);
            }

            return result;
        }

        public static IOrderedQueryable<T> Ordering<T>(IQueryable<T> input, OrderInput orderInput)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            return  orderInput.Order == OrderTypeEnum.Asc
                 ? OrderingHelper.OrderBy(input,orderInput.Property, parameter)
                 : OrderingHelper.OrderByDescending(input, orderInput.Property, parameter);

        }

    }
}
