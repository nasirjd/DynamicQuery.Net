using System.Linq.Expressions;
using DynamicQuery.Net.Utility.dto.input;
using DynamicQuery.Net.Utility.Interface;

namespace DynamicQuery.Net.Utility
{
    public class NormalCompare : ICompare
    {
        public Expression Equal<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.Equal(input.Property, valueExpr);
        }

        public Expression NotEqual<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.NotEqual(input.Property, valueExpr);
        }

        public Expression GreaterThan<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.GreaterThan(input.Property, valueExpr);
        }

        public Expression GreaterThanOrEqual<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.GreaterThanOrEqual(input.Property, valueExpr);
        }

        public Expression LessThan<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.LessThan(input.Property, valueExpr);
        }

        public Expression LessThanOrEqual<T>(CompareInput input)
        {
            var valueExpr = Expression.Constant(input.Value);
            return Expression.LessThanOrEqual(input.Property, valueExpr);
        }
    }
}
