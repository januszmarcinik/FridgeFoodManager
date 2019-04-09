using System.Collections.Generic;

namespace FridgeFoodManager.Tests.Integration.Contracts
{
    public class CommandSchema
    {
        public string Name { get; set; }

        public IEnumerable<CommandProperty> Properties { get; set; }
    }
}
