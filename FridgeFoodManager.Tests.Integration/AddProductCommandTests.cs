using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FridgeFoodManager.Api;
using FridgeFoodManager.Api.Commands.AddProduct;
using FridgeFoodManager.Tests.Integration.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Xunit;

namespace FridgeFoodManager.Tests.Integration
{
    public class AddProductCommandTests : IClassFixture<ApiFactory>
    {
        private const string _apiUrl = "api/commands/add-product";
        private readonly HttpClient _client;

        public AddProductCommandTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_IfExists_ShouldReturnCorrectCommandSchema()
        {
            var result = await _client.GetAsync(_apiUrl);
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

        [Fact]
        public async Task POST_IfModelIsCorrect_ShouldReturnAccepted()
        {
            SystemTime.NowFunc = () => new DateTime(2019, 4, 1);

            var command = new AddProductCommand
            {
                Name = "Mleko",
                ExpirationDate = new DateTime(2019, 4, 17),
                MaxDaysAfterOpening = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(_apiUrl, content);

            result.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }
    }
}
