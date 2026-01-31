using Microsoft.Data.SqlClient;

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
    }
}
