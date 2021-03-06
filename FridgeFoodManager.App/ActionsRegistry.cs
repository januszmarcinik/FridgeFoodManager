﻿using System;
using FridgeFoodManager.Domain;
using FridgeFoodManager.Domain.Commands.AddProduct;
using FridgeFoodManager.Domain.Commands.OpenProduct;
using FridgeFoodManager.Domain.Commands.RemoveProduct;
using FridgeFoodManager.Domain.Queries.GetAllProducts;
using FridgeFoodManager.Domain.Queries.GetOpenProducts;
using FridgeFoodManager.Domain.Queries.GetOverdueProducts;
using FridgeFoodManager.Domain.Queries.Shopping;

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

                case "remove":
                    RunCommand(new RemoveProductCommand());
                    break;

                case "get-all":
                    RunQuery(new GetAllProductsQuery());
                    break;

                case "get-open":
                    RunQuery(new GetOpenProductsQuery());
                    break;

                case "get-overdue":
                    RunQuery(new GetOverdueProductsQuery());
                    break;

                case "shopping":
                    RunQuery(new ShoppingQuery());
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
