namespace VentasBlazor.Web.Model.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }

        public List<ClienteCorreo> Correos { get; set; } = new List<ClienteCorreo>();
    }
}
