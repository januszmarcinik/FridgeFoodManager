using System;
using FridgeFoodManager.App.Infrastructure;
using FridgeFoodManager.Domain;

namespace FridgeFoodManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var efContext = new EfContext();
            var productsRepository = new ProductsRepository(efContext);
            productsRepository.InitializeMockData();
            var mediator = new Mediator(productsRepository);
            var actionsRegistry = new ActionsRegistry(mediator);

            while (true)
            {
                Console.Write("\nCommand: ");

                var command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                {
                    WriteError("Please choose command, query or pass 'exit' to leave an application.");
                    continue;
                }

                if (command.ToLower() == "exit")
                {
                    break;
                }

                try
                {
                    actionsRegistry.Run(command);
                    WriteSuccess();
                }
                catch (Exception e)
                {
                    WriteError(e.Message);
                }
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
