using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicQuery.Net.Utility
{
    public static class OrderingHelper
    {
        public static IOrderedQueryable<T> Ordering<T>(this IQueryable<T> source, string propertyName, string methodName)
        {
            Type type = typeof(T);
            ParameterExpression param = Expression.Parameter(type, "p");
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);
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
            var property = Expression.Property(parameter, propertyName);
            return source.OrderBy(Expression.Lambda<Func<T, IComparable>>(property, parameter));
        }
        public static IOrderedQueryable<T> OrderByDescending<T>( IQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, propertyName);
            return source.OrderByDescending(Expression.Lambda<Func<T, IComparable>>(property, parameter));
        }
        public static IOrderedQueryable<T> ThenBy<T>( IOrderedQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, propertyName);
            return source.ThenBy(Expression.Lambda<Func<T, IComparable>>(property, parameter));
        }
        public static IOrderedQueryable<T> ThenByDescending<T>( IOrderedQueryable<T> source, string propertyName, ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, propertyName);
            return source.ThenByDescending(Expression.Lambda<Func<T, IComparable>>(property, parameter));
        }

    }
}
