using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Order
{
    public record UpdateOrderResponse(
        Guid Id,
        decimal OrderTotalAmount,
        decimal OrderTotalDiscount,
        string Street,
        string City,
        string PostalCode,
        string Country,
        string ApplicationUserId,
        DateTime OrderDate,
        IEnumerable<UpdateOrderItemsRequest> OrderItems);

    public record UpdateOrderItemsResponse(
        Guid ProductId,
        int Quantity,
        decimal Price);
}
