using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class ClienteCommand
    {
        private readonly SQLServer _sqlServer;
        private readonly ClienteCorreoCommand _clienteCorreoCommand;
        public ClienteCommand(SQLServer sqlServer, ClienteCorreoCommand clienteCorreoCommand)
        {
            _sqlServer = sqlServer;
            _clienteCorreoCommand = clienteCorreoCommand;
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
        public async Task<int> InsertClienteTransactionAsync(Cliente cliente)
        {
            await using SqlConnection connection = _sqlServer.GetConnection();
            await using SqlTransaction transaction = await _sqlServer.CrearTransactionAsync(connection);//transaction
            try
            {
                var query = "INSERT INTO Clientes (Nombre, RFC) VALUES (@Nombre, @RFC); select Scope_Identity()";
                var parameters = new[]
                {
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@RFC", cliente.RFC)
            };
                int clienteId = await _sqlServer.ScalarAsync<int>(connection, transaction, query, parameters);  
                foreach (var correo in cliente.Correos)
                {
                    correo.ClienteId = clienteId;
                    await _clienteCorreoCommand.InsertClienteCorreoTransactionAsync(connection, transaction, correo);
                }
                transaction.Commit();
                return clienteId;
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw;
            }
            
        }
        public async Task<List<Cliente>> GetClientesAsync()
        {
            var query = "SELECT Id, Nombre, RFC FROM Clientes";
            return await _sqlServer.ReaderListAsync<Cliente>(query);
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
