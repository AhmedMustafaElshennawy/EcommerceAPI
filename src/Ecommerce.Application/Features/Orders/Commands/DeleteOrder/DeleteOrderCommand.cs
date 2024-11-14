using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid orderId):IRequest<ErrorOr<Unit>>;

}
