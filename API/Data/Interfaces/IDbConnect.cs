using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IDbConnect
    {
        Task<IEnumerable<T>> LoadData<T,U>(string sqlStatement, U parameters, string connectionString);
        Task SaveData<T>(string sqlStatement, T parameters, string connectionString);
    }
}