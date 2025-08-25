namespace Sodimac.Orders.Domain.Entities
{
    public class DeliveryStatus
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool EsEstadoFinal { get; set; }

        // Navegación
        public ICollection<DeliveryRoute> Rutas { get; set; } = new List<DeliveryRoute>();
    }
}
