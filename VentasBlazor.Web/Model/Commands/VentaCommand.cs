using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class VentaCommand
    {
        private readonly SQLServer _sqlServer;
        private readonly VentaDetalleCommand _detalleCommand;

        public VentaCommand(SQLServer sqlServer, VentaDetalleCommand detalleCommand)
        {
            _sqlServer = sqlServer;
            _detalleCommand = detalleCommand;
        }

        public async Task<int> InsertVentaTransactionAsync(Venta venta)
        {
            await using var connection = _sqlServer.GetConnection();
            await using var transaction = await _sqlServer.CrearTransactionAsync(connection);

            try
            {
                decimal totalCalculado = 0;

                // 🔥 PRIMERO calcular precios reales y subtotales
                foreach (var detalle in venta.Detalles)
                {
                    var productoQuery = "SELECT ValorUnitario FROM Productos WHERE Id = @Id";

                    var precio = await _sqlServer.ScalarAsync<decimal>(
                        connection,
                        transaction,
                        productoQuery,
                        new[] { new SqlParameter("@Id", detalle.ProductoId) });

                    detalle.PrecioUnitario = precio;
                    detalle.Subtotal = precio * detalle.Cantidad;

                    totalCalculado += detalle.Subtotal;
                }

                venta.Total = totalCalculado;

                // 🔥 AHORA sí insertar la venta con total correcto
                var query = @"INSERT INTO Ventas (Fecha, ClienteId, Total)
                              VALUES (@Fecha, @ClienteId, @Total);
                              SELECT SCOPE_IDENTITY();";

                var parameters = new[]
                {
                    new SqlParameter("@Fecha", venta.Fecha),
                    new SqlParameter("@ClienteId", venta.ClienteId),
                    new SqlParameter("@Total", venta.Total)
                };

                int ventaId = await _sqlServer.ScalarAsync<int>(
                    connection, transaction, query, parameters);

                // 🔥 Insertar detalles
                foreach (var detalle in venta.Detalles)
                {
                    detalle.VentaId = ventaId;

                    await _detalleCommand.InsertDetalleTransactionAsync(
                        connection, transaction, detalle);
                }

                transaction.Commit();
                return ventaId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}