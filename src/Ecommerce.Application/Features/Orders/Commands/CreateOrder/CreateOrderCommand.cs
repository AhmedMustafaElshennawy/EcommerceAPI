using Ecommerce.Domain.order;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand : IRequest<ErrorOr<Guid>>
    {
        public Guid ApplicationUserId { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public decimal OrderTotalDiscount { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
    public record OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
