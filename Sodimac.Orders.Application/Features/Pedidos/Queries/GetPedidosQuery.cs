using MediatR;
using Sodimac.Orders.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Queries
{
    public record GetPedidosQuery() : IRequest<IEnumerable<PedidoDto>>;
}
