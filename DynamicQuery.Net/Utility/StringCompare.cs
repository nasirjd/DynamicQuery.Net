using System.Linq.Expressions;
using DynamicQuery.Net.Utility.dto.input;
using DynamicQuery.Net.Utility.Interface;

namespace DynamicQuery.Net.Utility
{
    public class StringCompare : ICompare
    {
        private static int _equalNumber = 0;

        public Expression Equal<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, input.Property, value);
            return Expression.Equal(compare, Expression.Constant(_equalNumber));
        }

        public Expression NotEqual<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, input.Property, value);
            return Expression.NotEqual(compare, Expression.Constant(_equalNumber));
        }

        public Expression GreaterThan<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, input.Property, value);
            return Expression.GreaterThan(compare, Expression.Constant(_equalNumber));
        }

        public Expression GreaterThanOrEqual<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, new[] { (Expression)input.Property, value });
            return Expression.GreaterThanOrEqual(compare, Expression.Constant(_equalNumber));
        }

        public Expression LessThan<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, input.Property, value);
            return Expression.LessThan(compare, Expression.Constant(_equalNumber));
        }

        public Expression LessThanOrEqual<T>(CompareInput input)
        {
            var value = Expression.Constant(input.Value);
            var compare = Expression.Call(typeof(string), "Compare", null, new[] { (Expression)input.Property, value });
            return Expression.LessThanOrEqual(compare, Expression.Constant(_equalNumber));
        }
    }
}
