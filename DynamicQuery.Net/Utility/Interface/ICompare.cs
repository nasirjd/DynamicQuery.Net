using System.Linq.Expressions;
using DynamicQuery.Net.Utility.dto.input;

namespace DynamicQuery.Net.Utility.Interface
{
    public interface ICompare
    {
        Expression Equal<T>(CompareInput input);
        Expression NotEqual<T>(CompareInput input);
        Expression GreaterThan<T>(CompareInput input);
        Expression GreaterThanOrEqual<T>(CompareInput input);
        Expression LessThan<T>(CompareInput input);
        Expression LessThanOrEqual<T>(CompareInput input);
    }
}
