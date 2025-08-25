using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.DTOs
{
    public class CreatePedidoDto
    {
        public int ClienteId { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string? Observaciones { get; set; }
        public List<CreatePedidoProductoDto> Productos { get; set; } = new();

           public CreatePedidoDeliveryRouteDto? RutaEntrega { get; set; }
    }
}
