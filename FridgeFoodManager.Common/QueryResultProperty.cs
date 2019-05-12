using Newtonsoft.Json;

namespace FridgeFoodManager.Common
{
    public class QueryResultProperty
    {
        [JsonConstructor]
        public QueryResultProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}