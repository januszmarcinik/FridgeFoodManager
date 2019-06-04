using System;

namespace FridgeFoodManager.Domain.Commands.RemoveProduct
{
    public class RemoveProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
