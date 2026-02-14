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
    }
}
