using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Services
{
    public class ClienteService
    {
        private readonly ClienteCommand _clienteCommand;
        private readonly ClienteCorreoService _clienteCorreoService;

        public ClienteService(ClienteCommand clienteCommand, ClienteCorreoService clienteCorreoService)
        {
            _clienteCommand = clienteCommand;
            _clienteCorreoService = clienteCorreoService;
        }

        public async Task<int> CrearClienteAsync(Cliente cliente)
        {
            var clienteId = await _clienteCommand.InsertClienteAsync(cliente);
            foreach (var correo in cliente.Correos)
            {
                correo.ClienteId = clienteId;
                await _clienteCorreoService.InsertClienteCorreoAsync(correo);
            }
            return clienteId;
        }
        public async Task<int> CrearClienteTransactionAsync(Cliente cliente)
        {
            var clienteId = await _clienteCommand.InsertClienteTransactionAsync(cliente);
            return clienteId;
        }
        public async Task<List<Cliente>> ObtenerClientesAsync()
        {
            return await _clienteCommand.GetClientesAsync();
        }

        public async Task<List<ClienteCorreo>> ObtenerCorreosAsync(int clienteId)
        {
            return await _clienteCorreoService.ObtenerCorreosPorClienteIdAsync(clienteId);
        }
        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _clienteCommand.GetClientesAsync();
        }

        public async Task<List<ClienteCorreo>> GetCorreosByClienteIdAsync(int clienteId)
        {
            return await _clienteCommand.GetCorreosByClienteIdAsync(clienteId);
        }
    }
}
