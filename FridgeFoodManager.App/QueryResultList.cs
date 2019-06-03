using Newtonsoft.Json;

namespace FridgeFoodManager.App
{
    public class QueryResultList
    {
        [JsonConstructor]
        public QueryResultList(string[] columnNames, string[][] rowsValues)
        {
            ColumnNames = columnNames;
            RowsValues = rowsValues;
        }

        public string[] ColumnNames { get; }
        public string[][] RowsValues { get; }
    }
}