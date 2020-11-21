using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Infrastructure.Data
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<int> SaveData<T>(string sql, T parameters);
    }
}