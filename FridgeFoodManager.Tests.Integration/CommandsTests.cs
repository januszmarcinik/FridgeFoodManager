using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FridgeFoodManager.Tests.Integration
{
    public class CommandsTests : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public CommandsTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
            var result = await _client.GetAsync("api/commands/test");
            result.IsSuccessStatusCode.Should().BeTrue();

            var content = await result.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Ok");
        }

        [Fact]
        public async Task GET_CommandThatDoesNotExist_ShouldReturnNotFound()
        {
            var result = await _client.GetAsync("api/commands/some-undefined-command");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
