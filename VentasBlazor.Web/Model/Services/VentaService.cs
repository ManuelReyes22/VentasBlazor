using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Entities;
using VentasBlazor.Web.Model.DTOs;

namespace VentasBlazor.Web.Model.Services
{
    public class VentaService
    {
        private readonly VentaCommand _ventaCommand;

        public VentaService(VentaCommand ventaCommand)
        {
            _ventaCommand = ventaCommand;
        }

        public async Task<int> CrearVentaAsync(VentaDTO ventaDto)
        {
            // Convertir DTO a Entity
            var venta = new Venta
            {
                ClienteId = ventaDto.ClienteId,
                Fecha = ventaDto.Fecha
            };

            foreach (var detalleDto in ventaDto.Detalles)
            {
                venta.Detalles.Add(new VentaDetalle
                {
                    ProductoId = detalleDto.ProductoId,
                    Cantidad = detalleDto.Cantidad
                });
            }

            return await _ventaCommand.InsertVentaTransactionAsync(venta);
        }
    }
}
