using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FridgeFoodManager.Tests.Integration.Contracts;
using Newtonsoft.Json;
using Xunit;

namespace FridgeFoodManager.Tests.Integration
{
    public class AddProductCommandTests : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public AddProductCommandTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_IfExists_ShouldReturnCorrectCommandSchema()
        {
            var result = await _client.GetAsync("api/commands/add-product");
            result.IsSuccessStatusCode.Should().BeTrue();

            var content = await result.Content.ReadAsStringAsync();
            var schema = JsonConvert.DeserializeObject<CommandSchema>(content);

            schema.Name.Should().BeEquivalentTo("AddProductCommand");
            schema.Properties.Should().BeEquivalentTo(
                new CommandProperty
                {
                    Name = "Name",
                    PropertyType = CommandPropertyType.String,
                    IsRequired = true
                },
                new CommandProperty
                {
                    Name = "ExpirationDate",
                    PropertyType = CommandPropertyType.Date,
                    IsRequired = true
                },
                new CommandProperty
                {
                    Name = "MaxDaysAfterOpening",
                    PropertyType = CommandPropertyType.Int,
                    IsRequired = true
                }
            );
        }
    }
}
