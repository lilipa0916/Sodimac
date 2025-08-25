using AutoMapper;
using MediatR;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Features.Pedidos.Queries;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{
    public class GetPedidoByIdHandler : IRequestHandler<GetPedidoByIdQuery, PedidoDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPedidoByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PedidoDto?> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _unitOfWork.Repository<Pedido>()
                          .GetByIdAsync(request.PedidoId,
                              p => p.Cliente,                   
                              p => p.Productos,                 
                              p => p.RutaEntrega!,              
                              p => p.RutaEntrega!.Estado);      
            return pedido == null ? null : _mapper.Map<PedidoDto>(pedido);
        }
    }
}
