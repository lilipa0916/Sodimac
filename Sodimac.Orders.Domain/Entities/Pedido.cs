namespace Sodimac.Orders.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        public DateTime FechaEntrega { get; set; }
        public decimal MontoTotal { get; set; }
        public string? Observaciones { get; set; }

        public Cliente Cliente { get; set; } = null!;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

        public DeliveryRoute? RutaEntrega { get; set; }
    }
}
