using System;

namespace FridgeFoodManager.App
{
    class Program
    {
        private static readonly ApiClient ApiClient = new ApiClient();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("\nCommand: ");

                var command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                {
                    WriteError("Please choose command, query or pass 'exit' to leave an application.");
                    continue;
                }

                var parts = command.Split(" ");
                if (parts.Length == 1 && parts[0] == "exit")
                {
                    break;
                }
                if (parts.Length != 2)
                {
                    WriteError("Please pass two values: [command/query] [name].");
                    continue;
                }

                var type = parts[0].ToLower();
                var name = parts[1];

                if (type == "command" || type == "c" || type == "cmd")
                {
                    HandleCommand(name);
                }
                else if (type == "query" || type == "q")
                {
                    HandleQuery(name);
                }
            }
        }

        private static void HandleCommand(string name)
        {
            try
            {
                var url = $"commands/{name}";
                var commandSchema = ApiClient.GetCommandSchema(url);

                var commandSchemaDataProvider = new CommandSchemaDataProvider(commandSchema);
                var input = commandSchemaDataProvider.StartCreatingJsonContent();

                ApiClient.Post(url, input);
                WriteSuccess();
            }
            catch (Exception e)
            {
                WriteError(e.Message);
            }
        }

        private static void HandleQuery(string name)
        {
            try
            {
                var url = $"queries/{name}";
                var queryResultSchema = ApiClient.GetQueryResultSchema(url);

                var queryResultSchemaDataProvider = new QueryResultSchemaDataProvider(queryResultSchema);
                queryResultSchemaDataProvider.Write();
                WriteSuccess();
            }
            catch (Exception e)
            {
                WriteError(e.Message);
            }
        }

        private static void WriteSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSuccess");
            Console.WriteLine("******************************************************\n");
            Console.ResetColor();
        }

        private static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{error}");
            var stars = new string[error.Length];
            for (var i = 0; i < error.Length; i++)
            {
                stars[i] = "*";
            }
            Console.WriteLine($"{string.Concat(stars)}\n");
            Console.ResetColor();
        }
    }
}
