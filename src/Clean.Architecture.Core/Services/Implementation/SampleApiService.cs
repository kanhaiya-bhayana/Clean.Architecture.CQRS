using Clean.Architecture.Core.Entities.Data;
using Clean.Architecture.Core.Services.Interfaces;
using System.Net.Http.Json;

namespace Clean.Architecture.Core.Services.Implementation
{
    public class SampleApiService : ISampleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SampleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Product> GetProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("DummyJSON");
            var content = await client.GetFromJsonAsync<Product>($"/products");
            return content;
        }
    }
}
