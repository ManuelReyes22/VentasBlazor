using Microsoft.AspNetCore.Components;
using VentasBlazor.Web.Model.Entities;
using VentasBlazor.Web.Model.Services;

namespace VentasBlazor.Web.Components.Pages.Correos
{
    public partial class CorreoNuevo : ComponentBase
    {
        [Inject] private ClienteService ClienteService { get; set; } = default!;
        [Inject] private ClienteCorreoService ClienteCorreoService { get; set; } = default!;

        protected List<Cliente> _clientes = new();
        protected List<ClienteCorreo> _correos = new();

        protected ClienteCorreo _nuevoCorreo = new();
        protected int _clienteIdSeleccionado;
        protected string? _mensaje;

        protected override async Task OnInitializedAsync()
        {
            _clientes = await ClienteService.ObtenerClientesAsync();
        }

        protected async Task ClienteSeleccionado(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int clienteId))
            {
                _clienteIdSeleccionado = clienteId;

                _correos = await ClienteService.ObtenerCorreosAsync(clienteId);

                _nuevoCorreo = new ClienteCorreo();
                _mensaje = null;
            }
        }

        protected async Task GuardarCorreo()
        {
            if (_clienteIdSeleccionado == 0 ||
                string.IsNullOrWhiteSpace(_nuevoCorreo.Correo))
                return;

            _nuevoCorreo.ClienteId = _clienteIdSeleccionado;

            await ClienteCorreoService.InsertClienteCorreoAsync(_nuevoCorreo);

            _mensaje = "Correo guardado correctamente";

            _correos = await ClienteService
                .ObtenerCorreosAsync(_clienteIdSeleccionado);

            _nuevoCorreo = new ClienteCorreo();
        }
    }
}