using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Order
{
    public record GetOrderByIdResponse(
        Guid Id,
        decimal OrderTotalAmount,
        decimal OrderTotalDiscount,
        string Street,
        string City,
        string PostalCode,
        string Country,
        string ApplicationUserId,
        DateTime OrderDate,
        List<OrderItemResponse> OrderItems
    );

    public record OrderItemResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderItemResponse(Guid productId, int quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }

}
