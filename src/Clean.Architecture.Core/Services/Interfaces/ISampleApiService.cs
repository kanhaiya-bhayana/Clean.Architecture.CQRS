using Clean.Architecture.Core.Entities.Data;

namespace Clean.Architecture.Core.Services.Interfaces
{
    public interface ISampleApiService
    {
        Task<Product> GetProductsAsync();
    }
}
