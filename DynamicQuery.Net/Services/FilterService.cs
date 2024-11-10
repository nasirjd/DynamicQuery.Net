using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Utility;
using DynamicQuery.Net.Utility.dto.input;
using DynamicQuery.Net.Utility.Interface;
using Newtonsoft.Json.Linq;

namespace DynamicQuery.Net.Services
{
    public static class FilterService
    {
        private static readonly MethodInfo ToStringMethod = typeof(object).GetMethod("ToString");

        public static IQueryable<T> FilterByInputs<T>(IQueryable<T> dataInput, IEnumerable<FilterInput> filterInputs,
            OperationBetweenFiltersEnum operationBetweenFilters = OperationBetweenFiltersEnum.And)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            
            Expression resultExpr = Expression.Constant(operationBetweenFilters == OperationBetweenFiltersEnum.And);
            foreach (var filterInput in filterInputs)
            {
                Expression filterExpression = null;
                filterExpression = filterInput.Value is IEnumerable<object>
                    ? MultipleValueHandleExpression<T>(filterInput, parameter)
                    : FilterExpression<T>(filterInput, parameter);

                resultExpr = operationBetweenFilters == OperationBetweenFiltersEnum.And
                    ? Expression.AndAlso(resultExpr, filterExpression)
                    : Expression.Or(resultExpr, filterExpression);
            }

            return dataInput.Where(Expression.Lambda<Func<T, bool>>(resultExpr, parameter));
        }

        public static IQueryable<T> FilterByInput<T>(IQueryable<T> dataInput, FilterInput filterInput)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            if (filterInput.Value is IEnumerable<object>)
            {
                var valueArrayExpression = MultipleValueHandleExpression<T>(filterInput, parameter);
                if (valueArrayExpression != null)
                    return dataInput.Where(Expression.Lambda<Func<T, bool>>(valueArrayExpression, parameter));
            }

            return dataInput
                .Where(Expression.Lambda<Func<T, bool>>(FilterExpression<T>(filterInput, parameter), parameter));
        }

        private static Expression FilterExpression<T>(FilterInput filterInput,
            Expression parameter)
        {
            var property = Expression.Property(parameter, filterInput.Property);
            var value = Expression.Constant(filterInput.Value);

            ICompare compare;
            CompareInput compareInput = null;

            if (filterInput.Type == InputTypeEnum.Number &&
                (filterInput.Operation == OperationTypeEnum.StartWith ||
                 filterInput.Operation == OperationTypeEnum.Contain))
            {
                compare = new StringCompare();

                compareInput = new CompareInput
                {
                    Property = Expression.Call(property, ToStringMethod),
                    Value = value
                };
            }

            else
            {
                switch (filterInput.Type)
                {
                    case InputTypeEnum.Number:
                    case InputTypeEnum.Boolean:
                        compare = new NormalCompare();
                        break;
                    case InputTypeEnum.String:
                        compare = new StringCompare();
                        break;
                    default:
                        compare = new NormalCompare();
                        break;
                }

                compareInput = new CompareInput
                {
                    Property = property,
                    Value = value
                };
            }


            Expression resultExpr;
            switch (filterInput.Operation)
            {
                case OperationTypeEnum.Equal:
                    resultExpr = compare.Equal<T>(compareInput);
                    break;
                case OperationTypeEnum.NotEqual:
                    resultExpr = compare.NotEqual<T>(compareInput);
                    break;
                case OperationTypeEnum.GreaterThan:
                    resultExpr = compare.GreaterThan<T>(compareInput);
                    break;
                case OperationTypeEnum.GreaterThanOrEqual:
                    resultExpr = compare.GreaterThanOrEqual<T>(compareInput);
                    break;
                case OperationTypeEnum.LessThan:
                    resultExpr = compare.LessThan<T>(compareInput);
                    break;
                case OperationTypeEnum.LessThanOrEqual:
                    resultExpr = compare.LessThanOrEqual<T>(compareInput);
                    break;
                case OperationTypeEnum.Contain:
                    resultExpr = compare.Contains<T>(compareInput);
                    break;
                case OperationTypeEnum.StartWith:
                    resultExpr = compare.StartsWith<T>(compareInput);
                    break;
                default:
                    resultExpr = compare.Equal<T>(compareInput);
                    break;
            }

            return resultExpr;
        }


        private static Expression MultipleValueHandleExpression<T>(FilterInput filterInput,
            Expression parameter)
        {
            var stop = Stopwatch.StartNew();
            Expression valueArrayExpression = null;

            var isJArray = JArrayUtil.IsJArray(filterInput.Value);

            var values = filterInput.Value as IEnumerable<object>;
            if (values == null) return Expression.Empty();

            foreach (var value in values)
            {
                filterInput.Value = isJArray ? ((JValue)value).Value : value;
                if (valueArrayExpression == null)
                {
                    valueArrayExpression = FilterExpression<T>(filterInput, parameter);
                    continue;
                }

                if (filterInput.Operation == OperationTypeEnum.NotEqual)
                    valueArrayExpression = Expression.AndAlso(valueArrayExpression,
                        FilterExpression<T>(filterInput, parameter));
                else
                    valueArrayExpression = Expression.OrElse(valueArrayExpression,
                        FilterExpression<T>(filterInput, parameter));
            }

            stop.Stop();
            Trace.Write(stop.ElapsedTicks);
            return valueArrayExpression;
        }
    }

    public enum OperationBetweenFiltersEnum
    {
        And = 0,
        Or = 1
    }
}