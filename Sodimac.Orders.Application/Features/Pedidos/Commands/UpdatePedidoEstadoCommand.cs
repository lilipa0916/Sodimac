using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Commands
{
    public record UpdatePedidoEstadoCommand(int PedidoId, int NuevoEstadoId) : IRequest<bool>;
}
