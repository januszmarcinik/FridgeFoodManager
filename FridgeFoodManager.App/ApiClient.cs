using System;
using System.Net;
using System.Net.Http;
using System.Text;
using FridgeFoodManager.Common;
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

        public CommandSchema GetCommandSchema(string url)
        {
            var result = _httpClient.GetAsync($"{ApiUrl}/{url}").GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonConvert.DeserializeObject<CommandSchema>(content);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NullReferenceException("Given command schema was not found.");
            }

            throw new Exception(result.ReasonPhrase);
        }

        public QueryResultSchema GetQueryResultSchema(string url)
        {
            var result = _httpClient.GetAsync($"{ApiUrl}/{url}").GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return JsonConvert.DeserializeObject<QueryResultSchema>(content);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NullReferenceException("Given query result schema was not found.");
            }

            throw new Exception(result.ReasonPhrase);
        }

        public void Post<T>(string url, T model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync($"{ApiUrl}/{url}", content).GetAwaiter().GetResult();
            if (!result.IsSuccessStatusCode)
            {
                var error = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                throw new Exception(error);
            }
        }
    }
}
