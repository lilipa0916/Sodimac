using Sodimac.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.DTOs
{
    public class CreatePedidoDeliveryRouteDto
    {
        public string NombreRuta { get; set; } = null!;
        public int DeliveryStatusId { get; set; } = 1; // Default: Pendiente
        public string? Observaciones { get; set; }
    }
}