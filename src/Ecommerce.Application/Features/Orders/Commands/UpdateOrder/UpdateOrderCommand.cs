using Ecommerce.Domain.order;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{

    public record UpdateOrderCommand(
    Guid OrderId,
    decimal OrderTotalAmount,
    decimal OrderTotalDiscount,
    string Street,
    string City,
    string PostalCode,
    string Country,
    string ApplicationUserId,
    IEnumerable<UpdateOrderItemsDto> OrderResults) : IRequest<ErrorOr<Order>>;

    public record UpdateOrderItemsDto(
        Guid ProductId,
        int Quantity,
        decimal Price);
}
