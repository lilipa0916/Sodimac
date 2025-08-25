using FluentValidation;
using Sodimac.Orders.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Validators
{
    public class CreatePedidoProductoValidator : AbstractValidator<CreatePedidoProductoDto>
    {
        public CreatePedidoProductoValidator()
        {
            RuleFor(x => x.Producto)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("El nombre del producto es requerido y no debe exceder 100 caracteres");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThan(0)
                .WithMessage("El precio unitario debe ser mayor a 0");
        }
    }
}

