﻿using System.Collections.Generic;
using System.Linq;
using DynamicQuery.Net.Dto.Input;
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

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, IEnumerable<FilterInput> filterInputs)
        {
            return filterInputs != null ? FilterService.FilterByInputs(input, filterInputs) : input;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, FilterInput filterinput)
        {
            return filterinput != null ? FilterService.FilterByInput(input, filterinput) : input;
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, OrderFilterInput orderFilterInput)
        {
            return (IOrderedQueryable<T>)(orderFilterInput != null
                ? input.Filter(orderFilterInput.Filter).Order(orderFilterInput.Order)
                : input);
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input,
            OrderFilterNonFilterInput orderFilterInput)
        {
            return (IOrderedQueryable<T>)(orderFilterInput != null
                ? input.Filter(orderFilterInput.Filter).Order(orderFilterInput.Order)
                : input);
        }

        public static IOrderedQueryable<T> Filter<T>(this IQueryable<T> input, DynamicQueryNetInput dynamicInput)
        {
            return dynamicInput != null
                ? input.GlobalFilter(dynamicInput.GlobalFilter)
                    .Filter(dynamicInput.Filter).Order(dynamicInput.Order).Paging(dynamicInput.Paging)
                : (IOrderedQueryable<T>)input;
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, OrderInput orderInput)
        {
            return orderInput != null ? OrderService.Ordering(input, orderInput) : (IOrderedQueryable<T>)input;
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> input, List<OrderInput> orderInput)
        {
            return orderInput != null ? OrderService.Ordering(input, orderInput) : (IOrderedQueryable<T>)input;
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> input, PagingInput paging)
        {
            return paging != null ? input.Skip(paging.Page * paging.Size).Take(paging.Size) : input;
        }

        public static IOrderedQueryable<T> Paging<T>(this IOrderedQueryable<T> input, PagingInput paging)
        {
            return (IOrderedQueryable<T>)(paging != null
                ? input.Skip(paging.Page * paging.Size).Take(paging.Size)
                : input);
        }
    }
}