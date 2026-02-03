using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Services
{
    public class ClienteService
    {
        private readonly ClienteCommand _clienteCommand;

        public ClienteService(ClienteCommand clienteCommand)
        {
            _clienteCommand = clienteCommand;
        }

        public async Task<int> CrearClienteAsync(Cliente cliente)
        {
            return await _clienteCommand.InsertClienteAsync(cliente);
        }
    }
}
