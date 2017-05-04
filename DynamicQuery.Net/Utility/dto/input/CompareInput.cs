using System.Linq.Expressions;

namespace DynamicQuery.Net.Utility.dto.input
{
    public class CompareInput
    {
        public ParameterExpression Parameter { get; set; }
        public MemberExpression Property { get; set; }
        public ConstantExpression Value { get; set; }
    }
}