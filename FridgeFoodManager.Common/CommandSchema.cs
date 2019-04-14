using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FridgeFoodManager.Common
{
    public class CommandSchema
    {
        [JsonConstructor]
        public CommandSchema(string name, IEnumerable<CommandProperty> properties)
        {
            Name = name;
            Properties = properties;
        }

        public string Name { get; }

        public IEnumerable<CommandProperty> Properties { get; }

        public static CommandSchema FromCommand<TCommand>() where TCommand : ICommand
        {
            var type = typeof(TCommand);

            var properties = type
                .GetProperties()
                .Select(property => 
                    new CommandProperty(
                        name: property.Name,
                        propertyType: CommandProperty.GetPropertyTypeFrom(property.PropertyType),
                        isRequired: Nullable.GetUnderlyingType(property.PropertyType) == null
                    )
                );

            return new CommandSchema(type.Name, properties);
        }
    }
}
