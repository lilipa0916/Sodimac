using AutoMapper;
using MediatR;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoCommand, PedidoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePedidoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PedidoDto> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = _mapper.Map<Pedido>(request.Pedido);
            try
            {

                // Calcular monto total
                if (pedido.Productos?.Any() == true)
                {
                    pedido.MontoTotal = pedido.Productos.Sum(i => i.Subtotal);
                }

                await _unitOfWork.Repository<Pedido>().AddAsync(pedido);
                await _unitOfWork.SaveChangesAsync();

                if (request.Pedido.RutaEntrega != null)
                {
                    await CrearRutaEntrega(pedido.Id, request.Pedido.RutaEntrega);
                }
            }
            catch (Exception)
            {

                throw;
            }

            PedidoDto ped= _mapper.Map<PedidoDto>(pedido);
            return ped;
        }

        private async Task CrearRutaEntrega(int pedidoId, CreatePedidoDeliveryRouteDto rutaDto)
        {
            try
            {
                // Validar que el estado existe
                var estadosExistentes = await _unitOfWork.Repository<DeliveryStatus>().GetAllAsync();
                var estadoExiste = estadosExistentes.Any(e => e.Id == rutaDto.DeliveryStatusId);

                if (!estadoExiste)
                {
                    Console.WriteLine($"Warning: Estado {rutaDto.DeliveryStatusId} no existe, usando Pendiente (1)");
                    rutaDto.DeliveryStatusId = 1; // Default: Pendiente
                }

                var ruta = new DeliveryRoute
                {
                    PedidoId = pedidoId,
                    DeliveryStatusId = rutaDto.DeliveryStatusId,
                    NombreRuta = rutaDto.NombreRuta ?? $"Ruta-{pedidoId.ToString().PadLeft(8, '0')}",
                    FechaAsignacion = DateTime.UtcNow,
                    Observaciones = rutaDto.Observaciones
                };

                await _unitOfWork.Repository<DeliveryRoute>().AddAsync(ruta);
                await _unitOfWork.SaveChangesAsync();

                Console.WriteLine($"Ruta creada exitosamente para pedido {pedidoId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando ruta para pedido {pedidoId}: {ex.Message}");
                // No lanzar excepción para que el pedido se cree aunque falle la ruta
            }
        }
    }

}
