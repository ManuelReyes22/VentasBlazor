using Microsoft.Data.SqlClient;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Entities;

namespace VentasBlazor.Web.Model.Commands
{
    public class ProductoCommand
    {
        private readonly SQLServer _sqlServer;

        public ProductoCommand(SQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task<int> InsertProductoAsync(Producto producto)
        {
            var query = "INSERT INTO Productos (Codigo, Descripcion, ValorUnitario) VALUES (@Codigo, @Descripcion, @ValorUnitario)";
            var parameters = new[]
            {
                new SqlParameter("@Codigo", producto.Codigo),
                new SqlParameter("@Descripcion", producto.Descripcion),
                new SqlParameter("@ValorUnitario", producto.ValorUnitario)
            };
            return await _sqlServer.NonQueryAsync(query, parameters);
        }
        public async Task<List<Producto>> GetProductosAsync()
        {
            var query = "SELECT Id, Codigo, Descripcion, ValorUnitario FROM Productos";
            return await _sqlServer.ReaderListAsync<Producto>(query);
        }
    }
}
