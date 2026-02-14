using Microsoft.AspNetCore.Components;
using VentasBlazor.Web.Model.Entities;
using VentasBlazor.Web.Model.Services;

namespace VentasBlazor.Web.Components.Pages.Clientes
{
    public partial class ClienteNuevo
    {
        [Inject] private ClienteService ClienteService { get; set; } = default!;
        private Cliente _cliente = new Cliente();
        private string? _mensaje;

        private async Task GuardarCliente()
        {
            var correo = new ClienteCorreo { Correo = "manuel202reyes.com" };
            _cliente.Correos.Add(correo);

            var clienteId = await ClienteService.CrearClienteAsync(_cliente);
            if (clienteId > 0)
            {
                _mensaje = $"Cliente guardado correctamente {clienteId}";
                _cliente = new Cliente();
            }
        }
    }
}
