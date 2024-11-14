using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Products
{
    public record UpdateProductResponse(
    Guid productId,
    string Name,
    string Description,
    string PictureUrl,
    decimal Price,
    decimal Discount,
    Guid CategoryId);
}
