namespace Sodimac.Orders.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;

        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
