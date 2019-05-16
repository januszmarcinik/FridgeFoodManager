using System;
using Newtonsoft.Json;

namespace FridgeFoodManager.Common
{
    public class CommandProperty
    {
        [JsonConstructor]
        public CommandProperty(string name, CommandPropertyType propertyType, bool isRequired)
        {
            Name = name;
            PropertyType = propertyType;
            IsRequired = isRequired;
        }

        public string Name { get; }

        public CommandPropertyType PropertyType { get; }

        public bool IsRequired { get; }

        public static CommandPropertyType GetPropertyTypeFrom(Type type)
        {
            switch (type.Name)
            {
                case nameof(Int32): return CommandPropertyType.Int;
                case nameof(DateTime): return CommandPropertyType.Date;
                case nameof(Boolean): return CommandPropertyType.Bool;
                case nameof(Decimal): return CommandPropertyType.Decimal;
                case nameof(String): return CommandPropertyType.String;
                case nameof(Guid): return CommandPropertyType.Guid;
                default: throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
