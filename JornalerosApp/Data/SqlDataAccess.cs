using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            try
            {
                IEnumerable<T> data = new List<T>();
                string getStringConnection = _configuration.GetConnectionString("DefaultConnection");
                using IDbConnection dbConnection = new SqlConnection(getStringConnection);
                data = await dbConnection.QueryAsync<T>(sql, parameters);
                return data.Count() > 0 ? data.ToList() : null;
            }catch(Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }            
        }

        public async Task<int> SaveData<T>(string sql, T parameters)
        {
            try
            {
                string getStringConnection = _configuration.GetConnectionString("DefaultConnection");
                using IDbConnection dbConnection = new SqlConnection(getStringConnection);
                return await dbConnection.ExecuteAsync(sql, parameters);
            }catch(Exception exc)
            {
                Debug.WriteLine(exc.Message);
                throw;
            }
           
        }

        
    }
}
