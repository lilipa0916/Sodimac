using AutoMapper;
using MediatR;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{

    public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePedidoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _unitOfWork.Repository<Pedido>().GetByIdAsync(request.Pedido.Id);
            
            if (pedido == null) return false;
            var estadosExistentes = await _unitOfWork.Repository<DeliveryStatus>().GetAllAsync();
            var estadoExiste = estadosExistentes.Any(e => e.Id == request.Pedido.RutaEntrega.DeliveryStatusId);

            if (!estadoExiste)
            {
                Console.WriteLine($"Error: El estado {request.Pedido.RutaEntrega.DeliveryStatusId} no existe");
                return false;
            }

            if (pedido.RutaEntrega == null)
            {
                pedido.RutaEntrega = new DeliveryRoute
                {
                    PedidoId = pedido.Id,
                    DeliveryStatusId = request.Pedido.RutaEntrega.DeliveryStatusId,
                    NombreRuta = $"Ruta-{pedido.Id.ToString().PadLeft(8, '0')}",
                    FechaAsignacion = DateTime.UtcNow
                };

                await _unitOfWork.Repository<DeliveryRoute>().AddAsync(pedido.RutaEntrega);
            }
            else
            {
                pedido.RutaEntrega.DeliveryStatusId = request.Pedido.RutaEntrega.DeliveryStatusId;

                // Actualizar fecha si es necesario
                if (request.Pedido.RutaEntrega.DeliveryStatusId == 4) // Entregado
                {
                    pedido.RutaEntrega.FechaEntregaReal = DateTime.UtcNow;
                }

                _unitOfWork.Repository<DeliveryRoute>().Update(pedido.RutaEntrega);
            }


            _mapper.Map(request.Pedido, pedido);
            pedido.MontoTotal = pedido.Productos.Sum(i => i.Subtotal);

            _unitOfWork.Repository<Pedido>().Update(pedido);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
