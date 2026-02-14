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
            var registrosAfectados = await ClienteService.CrearClienteAsync(_cliente);
            if (registrosAfectados > 0)
            {
                _mensaje = "Cliente guardado correctamente";
                _cliente = new Cliente();
            }
        }
    }
}
