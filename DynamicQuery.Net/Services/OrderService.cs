using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Utility;

namespace DynamicQuery.Net.Services
{
    public static class OrderService
    {
        public static IOrderedQueryable<T> Ordering<T>(IQueryable<T> queryable, IEnumerable<OrderInput> orderInputs)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            IOrderedQueryable<T> result = null;
            var isFirst = true;
            foreach (var orderInput in orderInputs)
            {
                if (isFirst)
                {
                    result = orderInput.Order == OrderTypeEnum.Asc
                        ? OrderingHelper.OrderBy(queryable, orderInput.Property, parameter)
                        : OrderingHelper.OrderByDescending(queryable, orderInput.Property, parameter);
                    
                    isFirst = false;
                }
                else
                {
                    result = orderInput.Order == OrderTypeEnum.Asc
                        ? OrderingHelper.ThenBy(result, orderInput.Property, parameter)
                        : OrderingHelper.ThenByDescending(result, orderInput.Property, parameter);
                }
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
