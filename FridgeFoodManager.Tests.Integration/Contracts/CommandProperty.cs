namespace FridgeFoodManager.Tests.Integration.Contracts
{
    public class CommandProperty
    {
        public string Name { get; set; }

        public CommandPropertyType PropertyType { get; set; }

        public bool IsRequired { get; set; }
    }
}
