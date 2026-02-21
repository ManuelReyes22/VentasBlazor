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
    }
}
