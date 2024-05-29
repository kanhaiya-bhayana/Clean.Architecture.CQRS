using Clean.Architecture.Core.Entities.Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Repositories.Wrapper
{
    public interface IDbConnectionWrapper
    {
        Task<IEnumerable<Account>> QueryAsync(string sql, object param = null);
        Task<Account> QuerySingleOrDefaultAsync(string sql, object param = null);
        Task<int> ExecuteAsync(string sql, object param = null);
    }
}
