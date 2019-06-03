using System;

namespace FridgeFoodManager.Domain.Commands.OpenProduct
{
    public class OpenProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
