namespace Sodimac.Orders.Application.DTOs
{
    public class DeliveryStatusDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool EsEstadoFinal { get; set; }
    }
}
