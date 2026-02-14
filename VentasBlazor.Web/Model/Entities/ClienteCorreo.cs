namespace VentasBlazor.Web.Model.Entities
{
    public class ClienteCorreo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Correo { get; set; } = string.Empty;
    }
}
