using System;
using System.Collections.Generic;
using System.Linq;

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
            
            foreach (var queryResultList in _queryResultSchema.Lists)
            {
                var maxCharactersLengths = CountMaxCharactersLengths(queryResultList);

                var header = GetValuesWithPadding(queryResultList.ColumnNames, maxCharactersLengths);
                Console.WriteLine(header);

                foreach (var rowValues in queryResultList.RowsValues)
                {
                    var row = GetValuesWithPadding(rowValues, maxCharactersLengths);
                    Console.WriteLine(row);
                }
            }
        }

        private static int[] CountMaxCharactersLengths(QueryResultList queryResultList)
        {
            var maxCharactersLengths = queryResultList
                .ColumnNames
                .Select(x => x.Length)
                .ToArray();

            foreach (var rowValues in queryResultList.RowsValues)
            {
                for (var i = 0; i < maxCharactersLengths.Length; i++)
                {
                    if (rowValues[i].Length > maxCharactersLengths[i])
                    {
                        maxCharactersLengths[i] = rowValues[i].Length;
                    }
                }
            }

            return maxCharactersLengths;
        }

        private static string GetValuesWithPadding(string[] values, int[] maxCharactersLengths)
        {
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = values[i].PadRight(values[i].Length + maxCharactersLengths[i] - values[i].Length);
            }

            return string.Join("  ", values);
        }
    }
}