using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ErrorOr<Product>>;
}
