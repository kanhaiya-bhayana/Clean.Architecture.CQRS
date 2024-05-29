using Moq;
using System.Data;
namespace Clean.Architecture.Infrastructure.test.Helper
{
    public static class MoqDapperExtensions
    {
        /*public static void SetupDapperAsync<T>(this Mock<IDbConnection> mock, Func<IDbConnection, Task<IEnumerable<T>>> expression, IEnumerable<T> result) where T : class
        {
            mock.Setup(expression).ReturnsAsync(result);
        }

        public static void SetupDapperAsync<T>(this Mock<IDbConnection> mock, Func<IDbConnection, Task<T>> expression, T result) where T : class
        {
            mock.Setup(expression).ReturnsAsync(result);
        }

        public static void SetupDapperAsync(this Mock<IDbConnection> mock, Func<IDbConnection, Task<int>> expression, int result)
        {
            mock.Setup(expression).ReturnsAsync(result);
        }*/
    }
}
