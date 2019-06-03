using Newtonsoft.Json;

namespace FridgeFoodManager.App
{
    public class QueryResultProperty
    {
        public QueryResultProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}