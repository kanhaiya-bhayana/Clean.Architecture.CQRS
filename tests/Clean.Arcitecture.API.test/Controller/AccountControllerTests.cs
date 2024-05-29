using Clean.Architecture.API.Controllers;
using Clean.Architecture.Core.Accounts.Commands.Create;
using Clean.Architecture.Core.Accounts.Commands.Delete;
using Clean.Architecture.Core.Accounts.Commands.Update;
using Clean.Architecture.Core.Accounts.Queries.Get;
using Clean.Architecture.Core.Accounts.Queries.GetAll;
using Clean.Architecture.Core.Common.Request;
using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.Core.Common.Utility;
using MediatR;
using Microsoft.Data.SqlClient;
using Moq;

public class AccountControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AccountController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnSuccessResponse_WhenDataIsRetrieved()
    {
        // Arrange
        var responseData = new List<AccountResponse> { new AccountResponse() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAccountQuery>(), default)).ReturnsAsync(responseData);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<ApiResponse>(result);
        Assert.True(okResult.Success);
        Assert.Equal(responseData, okResult.Result);
    }


    [Fact]
    public async Task GetByAccountNumber_ShouldReturnSuccessResponse_WhenDataIsRetrieved()
    {
        // Arrange
        var accountNumber = 12345L;
        var responseData = new AccountResponse();
        _mediatorMock.Setup(m => m.Send(It.Is<GetAccountByNumberQuery>(q => q.AccountNumber == accountNumber), default))
                     .ReturnsAsync(responseData);

        // Act
        var result = await _controller.GetByAccountNumber(accountNumber);

        // Assert
        var okResult = Assert.IsType<ApiResponse>(result);
        Assert.True(okResult.Success);
        Assert.Equal(responseData, okResult.Result);
    }

    [Fact]
    public async Task CreateAccount_ShouldReturnSuccessResponse_WhenAccountIsCreated()
    {
        // Arrange
        var request = new AccountRequest();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), default)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateAccount(request);

        // Assert
        var okResult = Assert.IsType<ApiResponse>(result);
        Assert.True(okResult.Success);
        Assert.Equal(AccountConstants.MESSAGE_201, okResult.Message);
    }

    [Fact]
    public async Task UpdateAccount_ShouldReturnSuccessResponse_WhenAccountIsUpdated()
    {
        // Arrange
        var request = new AccountRequest();
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAccountCommand>(), default)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateAccount(request);

        // Assert
        var okResult = Assert.IsType<ApiResponse>(result);
        Assert.True(okResult.Success);
        Assert.Equal(AccountConstants.MESSAGE_200, okResult.Message);
    }

    [Fact]
    public async Task DeleteAccount_ShouldReturnSuccessResponse_WhenAccountIsDeleted()
    {
        // Arrange
        var accountNumber = 12345L;
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAccountCommand>(), default)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteAccount(accountNumber);

        // Assert
        var okResult = Assert.IsType<ApiResponse>(result);
        Assert.True(okResult.Success);
        Assert.Equal(AccountConstants.MESSAGE_200, okResult.Message);
    }

    [Fact]
    public async Task GetAll_ShouldReturnErrorResponse_WhenSqlExceptionIsThrown()
    {
        // Arrange
        var exceptionMessage = "Database error";
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAccountQuery>(), default))
                     .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var errorResult = Assert.IsType<ApiResponse>(result);
        Assert.False(errorResult.Success);
        Assert.Equal(exceptionMessage, errorResult.Message);
    }

    [Fact]
    public async Task GetAll_ShouldReturnErrorResponse_WhenExceptionIsThrown()
    {
        // Arrange
        var exceptionMessage = "General error";
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAccountQuery>(), default))
                     .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var errorResult = Assert.IsType<ApiResponse>(result);
        Assert.False(errorResult.Success);
        Assert.Equal(exceptionMessage, errorResult.Message);
    }


}
