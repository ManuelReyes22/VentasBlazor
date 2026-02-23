namespace VentasBlazor.Web.Model.DTOs
{
    public class VentaDTO
    {
        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public List<VentaDetalleDTO> Detalles { get; set; } = new();

        public decimal Total => Detalles.Sum(x => x.Subtotal);
    }
}
