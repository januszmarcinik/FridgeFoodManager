using System;

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
                    try
                    {
                        var url = $"commands/{name}";
                        var commandSchema = apiClient.GetCommandSchema(url);

                        var commandSchemaDataProvider = new CommandSchemaDataProvider(commandSchema);
                        var input = commandSchemaDataProvider.StartCreatingJsonContent();

                        apiClient.Post(url, input);
                        WriteSuccess();
                    }
                    catch (Exception e)
                    {
                        WriteError(e.Message);
                    }
                }
                else if (type == "query" || type == "q")
                {
                    try
                    {
                        var url = $"queries/{name}";
                        var queryResultSchema = apiClient.GetQueryResultSchema(url);

                        var queryResultSchemaDataProvider = new QueryResultSchemaDataProvider(queryResultSchema);
                        queryResultSchemaDataProvider.Write();
                        WriteSuccess();
                    }
                    catch (Exception e)
                    {
                        WriteError(e.Message);
                    }
                }
                else if (type == "exit")
                {
                    break;
                }

                Console.WriteLine("******************************************************\n\n");
            }
        }

        private static void WriteSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success");
            Console.ResetColor();
        }

        private static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}
