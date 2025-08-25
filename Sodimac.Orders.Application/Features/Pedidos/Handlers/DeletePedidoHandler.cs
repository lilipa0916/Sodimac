using MediatR;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{
    public class DeletePedidoHandler : IRequestHandler<DeletePedidoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePedidoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _unitOfWork.Repository<Pedido>().GetByIdAsync(request.PedidoId);
            if (pedido == null) return false;

            _unitOfWork.Repository<Pedido>().Remove(pedido);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
