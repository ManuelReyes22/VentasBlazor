using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Services
{
    public class ClienteCorreoService
    {
        private readonly ClienteCorreoCommand _clienteCorreoCommand;
        public ClienteCorreoService(ClienteCorreoCommand clienteCorreoCommand)
        {
            _clienteCorreoCommand = clienteCorreoCommand;
        }
        public async Task<int> InsertClienteCorreoAsync(ClienteCorreo clienteCorreo)
        {
            return await _clienteCorreoCommand.InsertClienteCorreoAsync(clienteCorreo);
        }
    }
}
