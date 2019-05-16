using System;
using FridgeFoodManager.Common;

namespace FridgeFoodManager.Api.Commands.OpenProduct
{
    public class OpenProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
