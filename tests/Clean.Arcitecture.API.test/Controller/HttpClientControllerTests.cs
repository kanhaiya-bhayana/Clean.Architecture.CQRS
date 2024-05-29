using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clean.Architecture.Core.Services.Interfaces;
using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.API.Controllers;
using Clean.Architecture.Core.Entities.Data;

public class HttpClientControllerTests
{
    private readonly Mock<ISampleApiService> _mockSampleApiService;
    private readonly HttpClientController _controller;

    /*public HttpClientControllerTests()
    {
        _mockSampleApiService = new Mock<ISampleApiService>();
        _controller = new HttpClientController(_mockSampleApiService.Object);
    }

    [Fact]
    public async Task GetProducts_ReturnsSampleProduct()
    {
       f

    [Fact]
    public async Task GetProducts_ReturnsNull_WhenNoProductFound()
    {
        // Arrange
        _mockSampleApiService.Setup(service => service.GetProductsAsync())
                             .ReturnsAsync((Product?)null);

        // Act
        var result = await _controller.GetProducts();

        // Assert
        Assert.Null(result);
    }*/
}
