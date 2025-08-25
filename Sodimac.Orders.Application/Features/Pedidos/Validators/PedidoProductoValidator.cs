using FluentValidation;
using Sodimac.Orders.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Validators
{
    public class PedidoProductoValidator : AbstractValidator<ProductoDto>
    {
        public PedidoProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Codigo).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Cantidad).GreaterThan(0);
            RuleFor(x => x.PrecioUnitario).GreaterThan(0);
        }
    }
}
