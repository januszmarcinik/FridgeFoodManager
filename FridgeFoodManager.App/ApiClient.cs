using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace FridgeFoodManager.App
{
    internal class ApiClient
    {
        private const string ApiUrl = "https://localhost:44358/api";
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public T Get<T>(string url)
        {
            var result = _httpClient.GetAsync($"{ApiUrl}/{url}").GetAwaiter().GetResult();
            result.EnsureSuccessStatusCode();

            var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void Post<T>(string url, T model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync($"{ApiUrl}/{url}", content).GetAwaiter().GetResult();
            result.EnsureSuccessStatusCode();
        }
    }
}
