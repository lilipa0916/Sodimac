using AutoMapper;
using MediatR;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Features.Pedidos.Queries;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{
    public class GetPedidosByClienteHandler : IRequestHandler<GetPedidosByClienteQuery, IEnumerable<PedidoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPedidosByClienteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDto>> Handle(GetPedidosByClienteQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _unitOfWork.Repository<Pedido>()
                         .FindAsync(p => p.ClienteId == request.ClienteId,
                             p => p.Cliente,
                             p => p.Productos,
                             p => p.RutaEntrega!,
                             p => p.RutaEntrega!.Estado);
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }
    }
}
