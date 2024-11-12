using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;


namespace DynamicQuery.Net.Services
{
    public static class GlobalFilterService
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<CustomProperty>> CustomPropertiesCache =
            new ConcurrentDictionary<Type, IEnumerable<CustomProperty>>();

        public static IQueryable<T> Filter<T>(IQueryable<T> queryable, string value)
        {
            var properties = GetTypeCachedCustomProperties<T>();

            var filters = properties.Select(p => new FilterInput
            {
                Property = p.Property,
                Type = p.Type,
                Value = value,
                Operation = OperationTypeEnum.Contain
            });

            return FilterService.FilterByInputs(queryable, filters, LogicalOperator.Or);
        }

        private static IEnumerable<CustomProperty> GetTypeCachedCustomProperties<T>()
        {
            return CustomPropertiesCache.GetOrAdd(typeof(T), GetPropertiesFromType);

            IEnumerable<CustomProperty> GetPropertiesFromType(Type t)
            {
                return t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => new CustomProperty
                    {
                        Property = p.Name,
                        Type = GetInputTypeEnum(p.PropertyType)
                    }).Where(p => p.Type != InputTypeEnum.None);
            }
        }

        private static InputTypeEnum GetInputTypeEnum(Type type)
        {
            if (type == typeof(string))
                return InputTypeEnum.String;

            if (type == typeof(int) || type == typeof(double) ||
                type == typeof(float) || type == typeof(decimal) || type == typeof(long))
                return InputTypeEnum.Number;

            return InputTypeEnum.None;
        }


        private class CustomProperty
        {
            public string Property { get; set; }
            public InputTypeEnum Type { get; set; }
        }
    }
}