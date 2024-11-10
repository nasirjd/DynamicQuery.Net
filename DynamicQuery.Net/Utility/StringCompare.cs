using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using DynamicQuery.Net.Utility.dto.input;
using DynamicQuery.Net.Utility.Interface;

namespace DynamicQuery.Net.Utility
{
    public class StringCompare : ICompare
    {
        private static int _equalNumber = 0;
        private const bool TrueValue = true;
        
        private static readonly MethodInfo CompareMethod = typeof(string).GetMethod("Compare", new[] { typeof(string), typeof(string) });
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly Expression NullExpression = Expression.Constant(null, typeof(string));

        public Expression Equal<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.Equal(compare, Expression.Constant(_equalNumber));
        }

        public Expression NotEqual<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.NotEqual(compare, Expression.Constant(_equalNumber));
        }

        public Expression GreaterThan<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.GreaterThan(compare, Expression.Constant(_equalNumber));
        }

        public Expression GreaterThanOrEqual<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.GreaterThanOrEqual(compare, Expression.Constant(_equalNumber));
        }

        public Expression LessThan<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.LessThan(compare, Expression.Constant(_equalNumber));
        }

        public Expression LessThanOrEqual<T>(CompareInput input)
        {
            var compare = Expression.Call(CompareMethod, input.Property, input.Value);
            return Expression.LessThanOrEqual(compare, Expression.Constant(_equalNumber));
        }

        public Expression Contains<T>(CompareInput input)
        {
            var nullCheck = Expression.Equal(input.Property, NullExpression);
            var contains = Expression.Call(input.Property, ContainsMethod, input.Value);
            var conditionalExpression = Expression.Condition(nullCheck, Expression.Constant(false), contains);
            return Expression.Equal(conditionalExpression, Expression.Constant(TrueValue));
        }

        public Expression StartsWith<T>(CompareInput input)
        {
            var nullCheck = Expression.Equal(input.Property, NullExpression);
            var startsWith = Expression.Call(input.Property, StartsWithMethod, input.Value);
            var conditionalExpression = Expression.Condition(nullCheck, Expression.Constant(false), startsWith);
            return Expression.Equal(conditionalExpression, Expression.Constant(TrueValue));
        }
    }
}