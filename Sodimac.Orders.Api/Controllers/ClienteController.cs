using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los clientes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
        {
            var clientes = await _unitOfWork.Repository<Cliente>().GetAllAsync();
            var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);
            return Ok(clientesDto);
        }

        /// <summary>
        /// Obtener cliente por ID
        /// </summary>
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<ClienteDto>> GetById(int id)
        //{
        //    var cliente = await _unitOfWork.Repository<Cliente>().GetByIdAsync(id);
        //    if (cliente == null) return NotFound();

        //    var clienteDto = _mapper.Map<ClienteDto>(cliente);
        //    return Ok(clienteDto);
        //}

        /// <summary>
        /// Crear nuevo cliente
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);

            await _unitOfWork.Repository<Cliente>().AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();

            return Ok(cliente.Id);
        }
    }
}
