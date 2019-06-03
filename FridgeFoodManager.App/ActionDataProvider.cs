using System;
using System.Reflection;

namespace FridgeFoodManager.App
{
    internal class ActionDataProvider
    {
        public static T ProvideValue<T>(T command)
        {
            var properties = command.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = GetPropertyValueByType(property);
                property.SetValue(command, propertyValue);
            }

            return command;
        }

        private static object GetPropertyValueByType(PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;
            var propertyName = propertyInfo.Name;
            var isRequired = Nullable.GetUnderlyingType(propertyType) == null;

            switch (propertyType.Name)
            {
                case nameof(Int32): return GetIntPropertyValue(propertyName, isRequired);
                case nameof(DateTime): return GetDatePropertyValue(propertyName, isRequired);
                case nameof(Boolean): return GetBoolPropertyValue(propertyName, isRequired);
                case nameof(Decimal): return GetDecimalPropertyValue(propertyName, isRequired);
                case nameof(String): return GetStringPropertyValue(propertyName, isRequired);
                case nameof(Guid): return GetGuidPropertyValue(propertyName, isRequired);

                default: throw new ArgumentOutOfRangeException(nameof(propertyType.Name));
            }
        }

        private static object GetStringPropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var value = Console.ReadLine();

            if (isRequired && string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"Property '{propertyName}' is required.");
                return GetStringPropertyValue(propertyName, true);
            }

            return value;
        }

        private static object GetDatePropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var isConvertSucceed = DateTime.TryParse(Console.ReadLine(), out var value);

            if (!isConvertSucceed && isRequired)
            {
                Console.WriteLine("Given date format is not valid.");
                return GetDatePropertyValue(propertyName, true);
            }

            return value;
        }

        private static object GetIntPropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var isConvertSucceed = int.TryParse(Console.ReadLine(), out var value);

            if (!isConvertSucceed && isRequired)
            {
                Console.WriteLine("Given number is not valid.");
                return GetIntPropertyValue(propertyName, true);
            }

            return value;
        }

        private static object GetBoolPropertyValue(string propertyName, bool isRequired)
        {
            throw new NotImplementedException();
        }

        private static object GetDecimalPropertyValue(string propertyName, bool isRequired)
        {
            throw new NotImplementedException();
        }

        private static object GetGuidPropertyValue(string propertyName, bool isRequired)
        {
            Console.Write($"{propertyName}: ");
            var isConvertSucceed = Guid.TryParse(Console.ReadLine(), out var value);

            if (!isConvertSucceed && isRequired)
            {
                Console.WriteLine("Given ID format is not valid.");
                return GetGuidPropertyValue(propertyName, true);
            }

            return value;
        }
    }
}
