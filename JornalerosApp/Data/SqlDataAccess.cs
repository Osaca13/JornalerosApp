using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornalerosApp.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;
        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string getStringConnection = _configuration.GetConnectionString("DefaultConnection");
            using (IDbConnection dbConnection = new SqlConnection(getStringConnection))
            {
                var tiempoDeEspera = dbConnection.ConnectionTimeout;
                var data = await dbConnection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string getStringConnection = _configuration.GetConnectionString("DataConnection");
            using (IDbConnection dbConnection = new SqlConnection(getStringConnection))
            {
                await dbConnection.ExecuteAsync(sql, parameters);                
            }
        }

        
    }
}
