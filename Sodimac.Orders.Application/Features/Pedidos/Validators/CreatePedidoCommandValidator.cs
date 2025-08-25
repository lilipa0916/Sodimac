using FluentValidation;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Application.Features.Pedidos.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Validators
{
    public class CreatePedidoCommandValidator : AbstractValidator<CreatePedidoCommand>
    {
        public CreatePedidoCommandValidator()
        {
            RuleFor(x => x.Pedido).NotNull().WithMessage("Los datos del pedido son requeridos");
            RuleFor(x => x.Pedido.ClienteId).NotEmpty().WithMessage("El ClienteId es requerido");
            RuleFor(x => x.Pedido.FechaEntrega).GreaterThan(DateTime.UtcNow).WithMessage("La fecha de entrega debe ser mayor a la fecha actual"); ;
            RuleFor(x => x.Pedido.Productos).NotEmpty().WithMessage("El pedido debe tener al menos un producto");
            RuleForEach(x => x.Pedido.Productos).SetValidator(new CreatePedidoProductoValidator());
        }
    }

}
