using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FridgeFoodManager.App
{
    public class QueryResultSchema
    {
        public QueryResultSchema(IEnumerable<QueryResultProperty> properties, IEnumerable<QueryResultList> lists)
        {
            Properties = properties;
            Lists = lists;
        }

        public IEnumerable<QueryResultProperty> Properties { get; }

        public IEnumerable<QueryResultList> Lists { get; }

        public static QueryResultSchema FromQueryResult<T>(T queryResult)
        {
            var type = typeof(T);

            var allProperties = type.GetProperties();

            var ordinaryProperties = allProperties
                .Where(property => !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                .Select(property => new QueryResultProperty(property.Name, property.GetValue(queryResult).ToString()));

            var enumerableProperties = allProperties
                .Where(property => typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                .Select(property =>
                {
                    var genericTypeProperties = property.PropertyType.GenericTypeArguments[0].GetProperties();

                    var headers = genericTypeProperties
                        .Select(x => x.Name)
                        .ToArray();

                    var enumerable = property.GetValue(queryResult) as IEnumerable<object>;
                    var rows = enumerable
                        .Select(obj => genericTypeProperties.Select(x => GetStringValueFrom(x.GetValue(obj))).ToArray())
                        .ToArray();

                    return new QueryResultList(headers, rows);
                });

            return new QueryResultSchema(ordinaryProperties, enumerableProperties);
        }

        private static string GetStringValueFrom(object value)
        {
            switch (value)
            {
                case DateTime date: return date.ToShortDateString();
                case bool logic: return logic ? "Yes" : "No";
                default: return value.ToString();
            }
        }
    }
}
