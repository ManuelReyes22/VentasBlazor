using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Services
{
    public class ProductoService
    {
        private readonly ProductoCommand _productoCommand;
        public ProductoService(ProductoCommand productoCommand)
        {
            _productoCommand = productoCommand;
        }
        public async Task<int> CrearProductoAsync(Producto producto)
        {
            return await _productoCommand.InsertProductoAsync(producto);
        }
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _productoCommand.GetProductosAsync();
        }
        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _productoCommand.GetProductosAsync();
        }
    }
}
