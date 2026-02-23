namespace VentasBlazor.Web.Model.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public decimal Total { get; set; }

        public List<VentaDetalle> Detalles { get; set; } = new();
    }
}