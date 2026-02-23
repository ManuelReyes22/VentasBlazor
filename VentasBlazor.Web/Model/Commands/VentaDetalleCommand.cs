using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class VentaDetalleCommand
    {
        private readonly SQLServer _sqlServer;

        public VentaDetalleCommand(SQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task InsertDetalleTransactionAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            VentaDetalle detalle)
        {
            var query = @"INSERT INTO VentaDetalle
                         (VentaId, ProductoId, Cantidad, PrecioUnitario, Subtotal)
                         VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal)";

            var parameters = new[]
            {
                new SqlParameter("@VentaId", detalle.VentaId),
                new SqlParameter("@ProductoId", detalle.ProductoId),
                new SqlParameter("@Cantidad", detalle.Cantidad),
                new SqlParameter("@PrecioUnitario", detalle.PrecioUnitario),
                new SqlParameter("@Subtotal", detalle.Subtotal)
            };

            await _sqlServer.NonQueryAsync(connection, transaction, query, parameters);
        }
    }
}
