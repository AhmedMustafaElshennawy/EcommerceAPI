using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Products.Commands.CreateProduct
{
        public record CreateProductCommand(
        string Name,
        string Description,
        IFormFile PictureUrl,
        decimal Price,
        decimal Discount,
        Guid CategoryId) : IRequest<ErrorOr<Product>>;
}
