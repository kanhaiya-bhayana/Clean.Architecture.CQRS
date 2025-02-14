using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Clean.Architecture.Core.Accounts.Commands.Update;
using Clean.Architecture.Core.Common.Mapper;
using Clean.Architecture.Core.Common.Request;
using Clean.Architecture.Core.Entities.Buisness;
using Clean.Architecture.Core.Interfaces;

public class UpdateAccountCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_UpdatesAccount()
    {
        // Arrange
        var accountRequest = new AccountRequest
        {
            CustomerId = 1,
            AccountType = "Savings",
            AccountNumber = 00000,
            BranchAddress = "Branch 1"
        };
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var command = new UpdateAccountCommand { AccountRequest = accountRequest };
        var handler = new UpdateAccountCommandHandler(unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        unitOfWorkMock.Verify(u => u.AccountRepository.UpdateAsync(It.IsAny<Account>()), Times.Once);
    }

    [Fact]
    public async Task Handle_NullCommand_ThrowsArgumentNullException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var handler = new UpdateAccountCommandHandler(unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
    }
}
