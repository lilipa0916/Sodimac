using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; } 
        public int PedidoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
        public bool Activo { get; set; } = true;
        public Pedido Pedido { get; set; } = null!;
    }
}
