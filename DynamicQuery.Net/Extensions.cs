using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Dto.Output;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Services;

namespace DynamicQuery.Net
{
    public static class Extensions
    {
        public static IQueryable<T> GlobalFilter<T>(this IQueryable<T> queryable, string searchTerm)
        {
            return !string.IsNullOrEmpty(searchTerm) && searchTerm.Length >= 3
                ? GlobalFilterService.Filter(queryable, searchTerm)
                : queryable;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, IEnumerable<FilterInput> filterInputs,
            LogicalOperator logicalOperator = LogicalOperator.And)
        {
            return filterInputs != null ? FilterService.FilterByInputs(input, filterInputs, logicalOperator) : input;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, FilterInput filterinput)
        {
            return filterinput != null ? FilterService.FilterByInput(input, filterinput) : input;
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, OrderFilterInput orderFilterInput)
        {
            return (IOrderedQueryable<T>)(orderFilterInput != null
                ? input
                    .Filter(orderFilterInput.PropertyFilters)
                    .Order(orderFilterInput.Orders)
                : input);
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input,
            OrderFilterMetaDataInput orderFilterInput)
        {
            return (IOrderedQueryable<T>)(orderFilterInput != null
                ? input
                    .Filter(orderFilterInput.PropertyFilters).Order(orderFilterInput.Orders)
                : input);
        }

        public static PaginationOutput<T> Filter<T>(this IQueryable<T> queryable, DynamicQueryNetInput dynamicInput)
        {
            if (dynamicInput == null)
                return queryable.RawPaginationOutput();


            if (dynamicInput.GlobalPropertyFilters != null || dynamicInput.PropertyFilters != null)
            {
                var parameter = Expression.Parameter(typeof(T), "p");

                Expression resultExpr = null;

                if (dynamicInput.GlobalPropertyFilters != null)
                    resultExpr =
                        FilterService.FilterByInputsExpression<T>(dynamicInput.GlobalPropertyFilters, parameter,
                            LogicalOperator.Or);

                if (dynamicInput.PropertyFilters != null)
                {
                    var filterExpression =
                        FilterService.FilterByInputsExpression<T>(dynamicInput.PropertyFilters, parameter);

                    resultExpr = resultExpr == null
                        ? filterExpression
                        : Expression.Or(
                            resultExpr,
                            filterExpression
                        );
                }

                if (resultExpr != null)
                    queryable = queryable.Where(Expression.Lambda<Func<T, bool>>(resultExpr, parameter));
            }


            return queryable.Order(dynamicInput.Orders)
                .Pagination(dynamicInput.Pagination);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput orderInput)
        {
            return orderInput != null ? OrderService.Ordering(input, orderInput) : (IOrderedQueryable<T>)input;
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, IEnumerable<OrderInput> orderInput)
        {
            return orderInput != null ? OrderService.Ordering(input, orderInput) : (IOrderedQueryable<T>)input;
        }

        public static PaginationOutput<T> Pagination<T>(this IOrderedQueryable<T> queryable, PaginationInput pagination)
        {
            return pagination != null
                ? new PaginationOutput<T>
                {
                    Count = queryable.Count(),
                    Data = (IOrderedQueryable<T>)queryable.Skip(pagination.Page * pagination.Size).Take(pagination.Size)
                }
                : queryable.RawPaginationOutput();
        }

        private static PaginationOutput<T> RawPaginationOutput<T>(this IQueryable<T> queryable)
        {
            return new PaginationOutput<T>
            {
                Count = queryable.Count(),
                Data = (IOrderedQueryable<T>)queryable
            };
        }
    }
}