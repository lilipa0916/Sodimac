namespace Sodimac.Orders.Application.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = null!;
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal MontoTotal { get; set; }
        public string? Observaciones { get; set; }

        public List<ProductoDto> Productos { get; set; } = new();
        public DeliveryRouteDto? RutaEntrega { get; set; }
    }
}
