using Clean.Architecture.Core.Entities.Buisness;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Infrastructure.Data.Queries;
using Clean.Architecture.Infrastructure.Repositories.Wrapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public class AccountRepository : IAccountRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IDbConnectionWrapper _connectionWrapper;

    public AccountRepository(IConfiguration configuration, IDbConnectionWrapper connectionWrapper)
    {
        _configuration = configuration;
        _connectionWrapper = connectionWrapper;
    }

    public async Task<IReadOnlyList<Account>> GetAllAsync()
    {
        var result = await _connectionWrapper.QueryAsync(AccountQueries.AllAccount);
        return result.ToList();
    }

    public async Task<Account> GetByIdAsync(long id)
    {
        var result = await _connectionWrapper.QuerySingleOrDefaultAsync(AccountQueries.AccountById, new { AccountNumber = id });
        if (result == null)
        {
            throw new Exception("Account not found");
        }
        return result;
    }

    public async Task AddAsync(Account entity)
    {
        await _connectionWrapper.ExecuteAsync(AccountQueries.AddAccount, entity);
    }

    public async Task UpdateAsync(Account entity)
    {
        entity.UpdatedAt = DateTime.Now;
        await _connectionWrapper.ExecuteAsync(AccountQueries.UpdateAccount, entity);
    }

    public async Task DeleteAsync(long id)
    {
        var result = await _connectionWrapper.ExecuteAsync(AccountQueries.DeleteAccount, new { AccountNumber = id });
        if (result == 0)
        {
            throw new Exception("Account not found");
        }
    }
}
