using Microsoft.AspNetCore.Components;
using VentasBlazor.Web.Model.DTOs;
using VentasBlazor.Web.Model.Entities;
using VentasBlazor.Web.Model.Services;

namespace VentasBlazor.Web.Components.Pages.Ventas
{
    public partial class VentaNueva
    {
        [Inject] private ClienteService _clienteService { get; set; } = default!;
        [Inject] private ProductoService _productoService { get; set; } = default!;
        [Inject] private VentaService _ventaService { get; set; } = default!;

        protected VentaDTO _venta = new();
        protected VentaDetalleDTO _nuevoDetalle = new();

        protected List<Cliente> _clientes = new();
        protected List<Producto> _productos = new();
        protected List<ClienteCorreo> _correos = new();

        protected string? _mensaje;

        protected override async Task OnInitializedAsync()
        {
            _clientes = await _clienteService.GetClientesAsync();
            _productos = await _productoService.GetProductosAsync();
        }

        protected async Task ClienteSeleccionado(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int clienteId))
            {
                _venta.ClienteId = clienteId;

                _correos = await _clienteService
                    .GetCorreosByClienteIdAsync(clienteId);
            }
            else
            {
                _venta.ClienteId = 0;
                _correos.Clear();
            }
        }
        protected void AgregarProducto()
        {
            if (_nuevoDetalle.ProductoId == 0 || _nuevoDetalle.Cantidad <= 0)
                return;

            var producto = _productos
                .FirstOrDefault(p => p.Id == _nuevoDetalle.ProductoId);

            if (producto is null)
                return;

            var detalle = new VentaDetalleDTO
            {
                ProductoId = producto.Id,
                Descripcion = producto.Descripcion,
                Cantidad = _nuevoDetalle.Cantidad,
                PrecioUnitario = producto.ValorUnitario
            };

            _venta.Detalles.Add(detalle);

            _nuevoDetalle = new VentaDetalleDTO();
        }

        protected void EliminarProducto(VentaDetalleDTO detalle)
        {
            _venta.Detalles.Remove(detalle);
        }
        protected async Task GuardarVenta()
        {
            try
            {
                if (_venta.ClienteId == 0 || !_venta.Detalles.Any())
                {
                    _mensaje = "Debe seleccionar cliente y agregar productos.";
                    return;
                }

                await _ventaService.CrearVentaAsync(_venta);

                _mensaje = "Venta guardada correctamente.";

                _venta = new VentaDTO();
                _nuevoDetalle = new VentaDetalleDTO();
                _correos.Clear();
            }
            catch (Exception ex)
            {
                _mensaje = "Error al guardar la venta: " + ex.Message;
            }
        }
    }
}