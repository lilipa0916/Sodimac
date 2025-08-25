using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Commands
{
    public record DeletePedidoCommand(int PedidoId) : IRequest<bool>;
}
