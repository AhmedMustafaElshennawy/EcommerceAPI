using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Products
{
    public record CreateProductResponse(
    Guid Id,
    string Name,
    string Description,
    string PictureUrl,
    decimal Price,
    decimal Discount,
    DateTime CreatedOn,
    Guid CategoryId);
}
