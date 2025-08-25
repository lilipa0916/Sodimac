using FluentValidation;
using Sodimac.Orders.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Application.Features.Pedidos.Validators
{
    public class UpdatePedidoValidator : AbstractValidator<PedidoDto>
    {
        public UpdatePedidoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ClienteId).NotEmpty();
            RuleFor(x => x.FechaEntrega).GreaterThan(DateTime.UtcNow);
            RuleFor(x => x.Productos).NotEmpty();
            RuleForEach(x => x.Productos).SetValidator(new PedidoProductoValidator());
        }
    }
}
