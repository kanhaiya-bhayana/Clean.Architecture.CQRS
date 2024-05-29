using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Clean.Architecture.API.Filter;
using Clean.Architecture.API.Controllers;
using System.Collections.Generic;

public class AuthorizationFilterAttributeTests
{
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthorizationFilterAttribute _authorizationFilter;

    public AuthorizationFilterAttributeTests()
    {
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConfiguration.SetupGet(c => c["SecretKeys:ApiKey"]).Returns("PrimaryApiKey");
        _mockConfiguration.SetupGet(c => c["SecretKeys:ApiKeySecondary"]).Returns("SecondaryApiKey1,SecondaryApiKey2");
        _mockConfiguration.SetupGet(c => c["SecretKeys:UseSecondaryKey"]).Returns("True");

        _authorizationFilter = new AuthorizationFilterAttribute(_mockConfiguration.Object);
    }

    private AuthorizationFilterContext CreateAuthorizationFilterContext(string authorizationHeader)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = authorizationHeader;
        var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
        return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
    }

    [Fact]
    public void OnAuthorization_ValidPrimaryApiKey_ShouldPass()
    {
        // Arrange
        var context = CreateAuthorizationFilterContext("PrimaryApiKey");

        // Act
        _authorizationFilter.OnAuthorization(context);

        // Assert
        Assert.Null(context.Result); // No result means authorization was successful
    }

    [Fact]
    public void OnAuthorization_ValidSecondaryApiKey_ShouldPass()
    {
        // Arrange
        var context = CreateAuthorizationFilterContext("SecondaryApiKey1");

        // Act
        _authorizationFilter.OnAuthorization(context);

        // Assert
        Assert.Null(context.Result); // No result means authorization was successful
    }

    [Fact]
    public void OnAuthorization_InvalidApiKey_ShouldFail()
    {
        // Arrange
        var context = CreateAuthorizationFilterContext("InvalidApiKey");

        // Act
        _authorizationFilter.OnAuthorization(context);

        // Assert
        Assert.IsType<UnauthorizedResult>(context.Result);
    }

    [Fact]
    public void OnAuthorization_NoApiKey_ShouldFail()
    {
        // Arrange
        var context = CreateAuthorizationFilterContext(string.Empty);

        // Act
        _authorizationFilter.OnAuthorization(context);

        // Assert
        Assert.IsType<UnauthorizedResult>(context.Result);
    }

    [Fact]
    public void OnAuthorization_SecondaryApiKeyNotAllowed_ShouldFail()
    {
        // Arrange
        _mockConfiguration.SetupGet(c => c["SecretKeys:UseSecondaryKey"]).Returns("False");
        var context = CreateAuthorizationFilterContext("SecondaryApiKey1");

        // Act
        _authorizationFilter.OnAuthorization(context);

        // Assert
        Assert.IsType<UnauthorizedResult>(context.Result);
    }
}
