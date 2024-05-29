using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.Core.Entities.Data;
using Clean.Architecture.Core.Services.Implementation;
using Moq;
using System.Net.Http.Json;

public class SampleApiServiceTests
{
    [Fact]
    public async Task GetProductsAsync_ReturnsSampleProduct()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Title = "Product 1", Description = "Description 1", Price = 10.99m }
        };

        var sampleProduct = new SampleProductResponse { Products = products };

        httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
        httpClientMock.Setup(client => client.GetFromJsonAsync<SampleProductResponse>(It.IsAny<string>(),  CancellationToken.None)).ReturnsAsync(sampleProduct);

        var sampleApiService = new SampleApiService(httpClientFactoryMock.Object);

        // Act
        var result = await sampleApiService.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetProductsAsync_ThrowsException_WhenHttpClientFails()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
        httpClientMock.Setup(client => client.GetFromJsonAsync<Product>(It.IsAny<string>(), CancellationToken.None)).ThrowsAsync(new HttpRequestException());

        var sampleApiService = new SampleApiService(httpClientFactoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => sampleApiService.GetProductsAsync());
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsNull_WhenApiResponseIsNull()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
        httpClientMock.Setup(client => client.GetFromJsonAsync<Product>(It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(value: null);

        var sampleApiService = new SampleApiService(httpClientFactoryMock.Object);

        // Act
        var result = await sampleApiService.GetProductsAsync();

        // Assert
        Assert.Null(result);
    }
}
