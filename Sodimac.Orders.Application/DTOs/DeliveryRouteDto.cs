using Sodimac.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.DTOs
{
    public class DeliveryRouteDto
    {
        public int Id { get; set; }
        public int DeliveryStatusId { get; set; }
        public string EstadoNombre { get; set; } = null!;
        public string NombreRuta { get; set; } = null!;
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaEntregaReal { get; set; }
        public string? Observaciones { get; set; }
    }
}