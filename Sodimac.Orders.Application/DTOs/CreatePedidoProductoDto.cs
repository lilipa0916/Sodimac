using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.DTOs
{
    public class CreatePedidoProductoDto
    {
        public string Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
