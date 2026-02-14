using Microsoft.Data.SqlClient;
using System.Data;

namespace VentasBlazor.Web.Model.Database
{
    public class SQLServer
    {
        private readonly string _connectionString;

        public SQLServer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<SqlTransaction> CrearTransactionAsync(SqlConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            return (SqlTransaction)await connection.BeginTransactionAsync();
        }

        public async Task<int> NonQueryAsync(string query, SqlParameter[] parameters = null)
        {
            await using var connection = GetConnection();
            await using var command = new SqlCommand(query, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        public async Task<int> NonQueryAsync(SqlConnection connection, SqlTransaction transaction, string query, SqlParameter[] parameters = null)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));

            await using var command = new SqlCommand(query, connection, transaction);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<T> ScalarAsync<T>(string query, SqlParameter[] parameters = null)
        {
            await using SqlConnection sqlConnection = GetConnection();
            await using SqlCommand sqlCommand = new(query, sqlConnection)
            {
                CommandType = CommandType.Text
            };

            if (parameters is not null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            await sqlConnection.OpenAsync();
            object result = await sqlCommand.ExecuteScalarAsync();

            return result is T value ? value : default;
        }

        public async Task<T> ScalarAsync<T>(SqlConnection connection, SqlTransaction transaction, string query, SqlParameter[] parameters = null)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            await using SqlCommand sqlCommand = new(query, connection, transaction)
            {
                CommandType = CommandType.Text
            };

            if (parameters is not null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            object result = await sqlCommand.ExecuteScalarAsync();

            return result is T value ? value : default;
        }
    }
}
