using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Order
{
    public record CreateOrderRequest(
         decimal OrderTotalAmount,
         decimal OrderTotalDiscount,
         string Street,
         string City,
         string PostalCode,
         string Country,
         Guid ApplicationUserId,
         List<OrderItemRequest> OrderItems
    );

    public record OrderItemRequest(
        Guid ProductId,
        int Quantity,
        decimal Price
    );
}
