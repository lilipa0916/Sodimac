using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using Sodimac.Orders.Application.Features.Pedidos.Queries;

namespace Sodimac.Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PedidoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Crear un nuevo pedido
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePedidoCommand command)
        {
            try
            {
                var pedidoId = await _mediator.Send(command);

                return StatusCode(201, pedidoId);
            }
            catch (Exception ex)
            {
                // Log del error para debugging
                Console.WriteLine($"Error creando pedido: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtener pedido por ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDto>> GetById(int id)
        {
            var pedido = await _mediator.Send(new GetPedidoByIdQuery(id));
            return pedido == null ? NotFound() : Ok(pedido);
        }

        /// <summary>
        /// Obtener todos los pedidos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll()
        {
            var pedidos = await _mediator.Send(new GetPedidosQuery());
            return Ok(pedidos);
        }

        /// <summary>
        /// Obtener pedidos por cliente
        /// </summary>
        [HttpGet("cliente/{clienteId:int}")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetByCliente(int clienteId)
        {
            var pedidos = await _mediator.Send(new GetPedidosByClienteQuery(clienteId));
            return Ok(pedidos);
        }

        /// <summary>
        /// Actualizar un pedido
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] PedidoDto pedidoDto)
        {
            if (id != pedidoDto.Id) return BadRequest("ID mismatch");

            var success = await _mediator.Send(new UpdatePedidoCommand(pedidoDto));
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Eliminar un pedido
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeletePedidoCommand(id));
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Actualizar estado de un pedido
        /// Id | Nombre      | Descripcion                      | EsEstadoFinal
        /// 1  | Pendiente   | Pedido creado, pendiente...      | 0
        /// 2  | Asignado    | Pedido asignado a ruta...        | 0  
        /// 3  | En Tránsito | Pedido en camino al cliente      | 0
        /// 4  | Entregado   | Pedido entregado exitosamente    | 1
        /// 5  | Cancelado   | Pedido cancelado                 | 1
        /// </summary>
        [HttpPatch("{id:int}/estado")]
        public async Task<ActionResult> UpdateEstado(int id, [FromBody] int nuevoEstadoId)
        {
            var success = await _mediator.Send(new UpdatePedidoEstadoCommand(id, nuevoEstadoId));
            return success ? NoContent() : NotFound();
        }
    }
}
