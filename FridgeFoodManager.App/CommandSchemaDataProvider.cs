using System;
using System.Linq;
using FridgeFoodManager.Common;
using Newtonsoft.Json.Linq;

namespace FridgeFoodManager.App
{
    internal class CommandSchemaDataProvider
    {
        private readonly CommandSchema _commandSchema;

        public CommandSchemaDataProvider(CommandSchema commandSchema)
        {
            _commandSchema = commandSchema;
        }

        public JObject StartCreatingJsonContent()
        {
            var propertyValues = _commandSchema.Properties
                .Select(commandProperty =>
                {
                    var propertyProvider = GetPropertyProviderByType(commandProperty.PropertyType);
                    return propertyProvider(commandProperty.Name, commandProperty.IsRequired);
                });

            return new JObject(propertyValues);
        }

        private static Func<string, bool, JProperty> GetPropertyProviderByType(CommandPropertyType commandPropertyType)
        {
            switch (commandPropertyType)
            {
                case CommandPropertyType.String:
                    return GetStringPropertyValue;
                case CommandPropertyType.Date:
                    return GetDatePropertyValue;
                case CommandPropertyType.Int:
                    return GetIntPropertyValue;
                case CommandPropertyType.Bool:
                    return GetBoolPropertyValue;
                case CommandPropertyType.Decimal:
                    return GetDecimalPropertyValue;
                default:
                    throw new NotImplementedException();
            }
        }

        private static JProperty GetStringPropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var value = Console.ReadLine();

            if (isRequired && string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"Property '{propertyName}' is required.");
                return GetStringPropertyValue(propertyName, true);
            }

            return new JProperty(propertyName, value);
        }

        private static JProperty GetDatePropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var isConvertSucceed = DateTime.TryParse(Console.ReadLine(), out var value);

            if (!isConvertSucceed && isRequired)
            {
                Console.WriteLine("Given date format is not valid.");
                return GetDatePropertyValue(propertyName, true);
            }

            return new JProperty(propertyName, value);
        }

        private static JProperty GetIntPropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var isConvertSucceed = int.TryParse(Console.ReadLine(), out var value);

            if (!isConvertSucceed && isRequired)
            {
                Console.WriteLine("Given number is not valid.");
                return GetIntPropertyValue(propertyName, true);
            }

            return new JProperty(propertyName, value);
        }

        private static JProperty GetBoolPropertyValue(string propertyName, bool isRequired)
        {
            throw new NotImplementedException();
        }

        private static JProperty GetDecimalPropertyValue(string propertyName, bool isRequired)
        {
            throw new NotImplementedException();
        }
    }
}
