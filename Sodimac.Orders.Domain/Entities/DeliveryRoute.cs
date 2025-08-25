namespace Sodimac.Orders.Domain.Entities
{
    public class DeliveryRoute
    {
        public int Id { get; set; } 
        public int PedidoId { get; set; }
        public int DeliveryStatusId { get; set; }
        public string NombreRuta { get; set; } = null!;
        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaEntregaReal { get; set; }
        public string? Observaciones { get; set; }

        // Navegación
        public Pedido Pedido { get; set; } = null!;
        public DeliveryStatus Estado { get; set; } = null!;
    }
}
