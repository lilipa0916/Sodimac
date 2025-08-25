using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
