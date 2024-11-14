using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Products
{
    public record CreateProductRequest(
        string Name, 
        string Description, 
        IFormFile PictureUrl,
        decimal Price, 
        decimal Discount, 
        Guid CategoryId);
}
