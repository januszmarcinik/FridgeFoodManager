using System;
using FridgeFoodManager.Domain;
using FridgeFoodManager.Domain.Commands.AddProduct;
using FridgeFoodManager.Domain.Commands.OpenProduct;
using FridgeFoodManager.Domain.Queries.GetAllProducts;
using FridgeFoodManager.Domain.Queries.GetOverdueProducts;

namespace FridgeFoodManager.App
{
    internal class ActionsRegistry
    {
        private readonly IMediator _mediator;

        public ActionsRegistry(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Run(string action)
        {
            switch (action)
            {
                case "add":
                    RunCommand(new AddProductCommand());
                    break;

                case "open":
                    RunCommand(new OpenProductCommand());
                    break;

                case "get-all":
                    RunQuery(new GetAllProductsQuery());
                    break;

                case "get-overdue":
                    RunQuery(new GetOverdueProductsQuery());
                    break;

                default:
                    throw new Exception($"Given command was not found: {action}.");
            }
        }

        private void RunCommand<T>(T command) where T : ICommand
        {
            ActionDataProvider.ProvideValue(command);
            var result = _mediator.Command(command);
            if (result.IsFailure)
            {
                throw new Exception(result.ErrorMessage);
            }
        }

        private void RunQuery<T>(IQuery<T> query) where T : class
        {
            ActionDataProvider.ProvideValue(query);
            var result = _mediator.Query(query);
            var queryResultSchema = QueryResultSchema.FromQueryResult(result);
            new QueryResultSchemaDataProvider(queryResultSchema).Write();
        }
    }
}
