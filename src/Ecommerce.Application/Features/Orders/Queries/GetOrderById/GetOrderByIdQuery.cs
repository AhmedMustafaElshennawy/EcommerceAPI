using Ecommerce.Domain.order;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<ErrorOr<Order>>;
}
