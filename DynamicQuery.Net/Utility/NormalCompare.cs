using System.Linq.Expressions;
using DynamicQuery.Net.Utility.dto.input;
using DynamicQuery.Net.Utility.Interface;

namespace DynamicQuery.Net.Utility
{
    public class NormalCompare : ICompare
    {
        public Expression Equal<T>(CompareInput input)
        {
            return Expression.Equal(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }

        public Expression NotEqual<T>(CompareInput input)
        {
            return Expression.NotEqual(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }

        public Expression GreaterThan<T>(CompareInput input)
        {
            return Expression.GreaterThan(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }

        public Expression GreaterThanOrEqual<T>(CompareInput input)
        {
            return Expression.GreaterThanOrEqual(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }

        public Expression LessThan<T>(CompareInput input)
        {
            return Expression.LessThan(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }

        public Expression LessThanOrEqual<T>(CompareInput input)
        {
            return Expression.LessThanOrEqual(input.Property, Expression.Convert(input.Value , input.Property.Type));
        }
    }
}
