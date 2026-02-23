using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class ClienteCorreoCommand
    {
        private readonly SQLServer _sqlServer;
        public ClienteCorreoCommand(SQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }
        public async Task<int> InsertClienteCorreoAsync(ClienteCorreo clienteCorreo)
        {
            var query = "INSERT INTO ClientesCorreos (ClienteId, Correo) VALUES (@ClienteId, @Correo)";
            var parameters = new[]
            {
                new SqlParameter("@ClienteId", clienteCorreo.ClienteId),
                new SqlParameter("@Correo", clienteCorreo.Correo)
            };
            return await _sqlServer.NonQueryAsync(query, parameters);
        }
        public async Task<int> InsertClienteCorreoTransactionAsync(SqlConnection connection, SqlTransaction transaction, ClienteCorreo clienteCorreo)
        {
            var query = "INSERT INTO ClientesCorreos (ClienteId, Correo) VALUES (@ClienteId, @Correo)";
            var parameters = new[]
            {
                new SqlParameter("@ClienteId", clienteCorreo.ClienteId),
                new SqlParameter("@Correo", clienteCorreo.Correo)
            };
            return await _sqlServer.NonQueryAsync(connection, transaction, query, parameters);
        }
        public async Task<List<ClienteCorreo>> GetCorreosByClienteIdAsync(int clienteId)
        {
            var query = "SELECT Id, ClienteId, Correo FROM ClientesCorreos WHERE ClienteId = @ClienteId";

            var parameters = new[]
            {
        new SqlParameter("@ClienteId", clienteId)
    };

            return await _sqlServer.ReaderListAsync<ClienteCorreo>(query, parameters);
        }
    }
}
