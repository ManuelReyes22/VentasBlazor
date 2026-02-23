namespace VentasBlazor.Web.Model.DTOs
{
    public class VentaDetalleDTO
    {
        public int ProductoId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
