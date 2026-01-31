using Microsoft.AspNetCore.Components;
using VentasBlazor.Web.Model.Entities;
using VentasBlazor.Web.Model.Services;

namespace VentasBlazor.Web.Components.Pages.Productos
{
    public partial class ProductoNuevo
    {
        [Inject] private ProductoService ProductoService { get; set; } = default!;
        private Producto _producto = new();
        private string? _mensaje;

        private async Task GuardarProducto()
        {
            var id = await ProductoService.CrearProductoAsync(_producto);

            if (id > 0)
            {
                _mensaje = "Producto guardado correctamente";
                _producto = new Producto(); // limpia el formulario
            }
        }
    }
}
