
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Order
{
    public record UpdateOrderRequest(
        Guid OrderId,
        decimal OrderTotalAmount,
        decimal OrderTotalDiscount,
        string Street,
        string City,
        string PostalCode,
        string Country,
        string ApplicationUserId,
        IEnumerable<UpdateOrderItemsRequest> OrderResults);

    public record UpdateOrderItemsRequest(
        Guid ProductId,
        int Quantity,
        decimal Price);

}
