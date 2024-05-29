using Clean.Architecture.Core.Entities.Data;
using Clean.Architecture.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {
        private readonly ISampleApiService _sampleApiService;
        public HttpClientController(ISampleApiService sampleApiService)
        {
            _sampleApiService = sampleApiService;
        }

        [HttpGet("getProducts")]
        public async Task<Product?> GetProducts()
        {
            var content = await _sampleApiService.GetProductsAsync();

            return content;
        }
    }
}
