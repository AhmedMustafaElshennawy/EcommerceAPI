using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
       Guid productId,
       string Name,
       string Description,
       IFormFile PictureUrl,
       decimal Price,
       decimal Discount,
       Guid CategoryId) : IRequest<ErrorOr<Product>>;
}
