using System;
using FridgeFoodManager.Common;

namespace FridgeFoodManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiClient = new ApiClient();

            while (true)
            {
                Console.Write("Command: ");
                var command = Console.ReadLine();
                var parts = command.Split(" ");

                var type = parts[0].ToLower();
                var name = parts[1];

                if (type == "command" || type == "c" || type == "cmd")
                {
                    var url = $"commands/{name}";
                    var commandSchema = apiClient.Get<CommandSchema>(url);

                    var commandSchemaDataProvider = new CommandSchemaDataProvider(commandSchema);
                    var input = commandSchemaDataProvider.StartCreatingJsonContent();

                    apiClient.Post(url, input);
                }
                else if (type == "exit")
                {
                    break;
                }
            }
        }
    }
}
