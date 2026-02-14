using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class ClienteCommand
    {
        private readonly SQLServer _sqlServer;

        public ClienteCommand(SQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task<int> InsertClienteAsync(Cliente cliente)
        {
            var query = "INSERT INTO Clientes (Nombre, RFC) VALUES (@Nombre, @RFC); select Scope_Identity()";
            var parameters = new[]
            {
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@RFC", cliente.RFC)
            };
            return await _sqlServer.ScalarAsync<int>(query, parameters);
        }
    }
}
