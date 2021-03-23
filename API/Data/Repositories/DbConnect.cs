using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using API.Data.Interfaces;

namespace API.Data.Repositories
{
    public class DbConnect : IDbConnect
    {
        public async Task<IEnumerable<T>> LoadData<T,U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlStatement, parameters);
                return rows;
            }
        }

        public async Task SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlStatement, parameters);
            }
        }
    }
}