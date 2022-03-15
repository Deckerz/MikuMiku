using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public class PrefixDatabase
{
    private readonly IConfiguration _config;
        public string ConnectionStringName => "PFDB";

        public PrefixDatabase(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T>(string sql)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, commandTimeout: 90);
                return data.ToList();
            }
        }

        public async Task<List<T>> LoadData<T, TU>(string sql, TU parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters, commandTimeout: 90);
                return data.ToList();
            }
        }


        public async Task<T> LoadDataSingle<T>(string sql)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql);
                return data.ToList().FirstOrDefault();
            }
        }

        public async Task<T> LoadDataSingle<T, TU>(string sql, TU parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList().FirstOrDefault();
            }
        }

        public async Task<bool> SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters, commandTimeout: 90);
            }

            return true;
        }
}