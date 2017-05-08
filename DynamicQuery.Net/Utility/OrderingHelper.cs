using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicQuery.Net.Utility
{
    public static class OrderingHelper
    {
        private static IOrderedQueryable<T> Ordering<T>(IQueryable<T> source, ParameterExpression parameter, string propertyName, string methodName)
        {
            Type type = typeof(T);
            MemberExpression property = Expression.PropertyOrField(parameter, propertyName);
            LambdaExpression sort = Expression.Lambda(property, parameter);
            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { type, property.Type },
                source.Expression,
                Expression.Quote(sort));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> OrderBy<T>( IQueryable<T> source, string propertyName , ParameterExpression parameter)
        {
            return Ordering(source,parameter ,propertyName, "OrderBy" );
        }
        public static IOrderedQueryable<T> OrderByDescending<T>( IQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            return Ordering(source, parameter, propertyName, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>( IOrderedQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            return Ordering(source, parameter, propertyName, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>( IOrderedQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            return Ordering(source, parameter, propertyName, "ThenByDescending");
        }

    }
}
