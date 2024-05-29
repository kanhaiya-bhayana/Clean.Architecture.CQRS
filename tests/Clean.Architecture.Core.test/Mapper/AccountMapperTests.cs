using Xunit;
using Clean.Architecture.Core.Common.Mapper;
using Clean.Architecture.Core.Common.Request;
using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.Core.Entities.Buisness;

public class AccountMapperTests
{
    [Fact]
    public void MapToAccountResponse_NullProperties_ReturnsAccountResponseWithDefaultValues()
    {
        // Arrange
        var account = new Account(); 

        // Act
        var accountResponse = AccountMapper.MapToAccountResponse(account);

        // Assert
        Assert.NotNull(accountResponse);
    }
}
