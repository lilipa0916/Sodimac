using AutoMapper;
using MediatR;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Commands
{
    public record CreatePedidoCommand(CreatePedidoDto Pedido) : IRequest<PedidoDto>;

}
