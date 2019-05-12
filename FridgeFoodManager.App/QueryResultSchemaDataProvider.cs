using System;
using System.Collections.Generic;
using System.Linq;
using FridgeFoodManager.Common;

namespace FridgeFoodManager.App
{
    internal class QueryResultSchemaDataProvider
    {
        private readonly QueryResultSchema _queryResultSchema;

        public QueryResultSchemaDataProvider(QueryResultSchema queryResultSchema)
        {
            _queryResultSchema = queryResultSchema;
        }

        public void Write()
        {
            foreach (var queryResultProperty in _queryResultSchema.Properties)
            {
                Console.WriteLine($"{queryResultProperty.Name}: {queryResultProperty.Value}");
            }

            Console.WriteLine();

            var maxCharactersLength = CountMaxCharactersLength();
            
            foreach (var queryResultList in _queryResultSchema.Lists)
            {
                var header = GetValuesWithPadding(queryResultList.ColumnNames, maxCharactersLength);
                Console.WriteLine(header);

                foreach (var rowValues in queryResultList.RowsValues)
                {
                    var row = GetValuesWithPadding(rowValues, maxCharactersLength);
                    Console.WriteLine(row);
                }
            }
        }

        private int CountMaxCharactersLength()
            => _queryResultSchema.Lists
                .SelectMany(x => x.ColumnNames)
                .Concat(_queryResultSchema.Lists.SelectMany(x => x.RowsValues.SelectMany(y => y)))
                .Select(x => x.Length)
                .Max();

        private static string GetValuesWithPadding(IEnumerable<string> values, int maxCharactersLength)
            => string.Join(" ", values.Select(x => x.PadRight(x.Length + maxCharactersLength - x.Length)));
    }
}