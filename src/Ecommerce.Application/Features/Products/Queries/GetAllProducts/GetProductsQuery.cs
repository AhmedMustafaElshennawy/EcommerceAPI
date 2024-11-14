using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts
{
    public record GetProductsQuery() : IRequest<ErrorOr<IEnumerable<Product>>>;
}
