using MediatR;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Handlers
{
    public class UpdatePedidoEstadoHandler : IRequestHandler<UpdatePedidoEstadoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePedidoEstadoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePedidoEstadoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _unitOfWork.Repository<Pedido>()
                        .GetByIdAsync(request.PedidoId,
                            p => p.Cliente,
                            p => p.Productos,
                            p => p.RutaEntrega!,
                            p => p.RutaEntrega!.Estado);
            if (pedido == null) return false;

            // 2. IMPORTANTE: Verificar que el estado existe antes de usarlo
            var estadosExistentes = await _unitOfWork.Repository<DeliveryStatus>().GetAllAsync();
            var estadoExiste = estadosExistentes.Any(e => e.Id == request.NuevoEstadoId);

            if (!estadoExiste)
            {
                Console.WriteLine($"Error: El estado {request.NuevoEstadoId} no existe");
                return false;
            }

            // 3. Crear o actualizar la ruta de entrega
            if (pedido.RutaEntrega == null)
            {
                // CREAR NUEVA RUTA - Solo referenciar el estado existente
                pedido.RutaEntrega = new DeliveryRoute
                {
                    PedidoId = pedido.Id,
                    DeliveryStatusId = request.NuevoEstadoId, // ← Solo el ID, no el objeto
                    NombreRuta = $"Ruta-{pedido.Id.ToString().PadLeft(8, '0')}",
                    FechaAsignacion = DateTime.UtcNow
                    // NO asignar Estado aquí, EF lo resolverá automáticamente
                };

                await _unitOfWork.Repository<DeliveryRoute>().AddAsync(pedido.RutaEntrega);
            }
            else
            {
                // ACTUALIZAR RUTA EXISTENTE - Solo cambiar el ID del estado
                pedido.RutaEntrega.DeliveryStatusId = request.NuevoEstadoId;

                // Actualizar fecha si es necesario
                if (request.NuevoEstadoId == 4) // Entregado
                {
                    pedido.RutaEntrega.FechaEntregaReal = DateTime.UtcNow;
                }

                _unitOfWork.Repository<DeliveryRoute>().Update(pedido.RutaEntrega);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
