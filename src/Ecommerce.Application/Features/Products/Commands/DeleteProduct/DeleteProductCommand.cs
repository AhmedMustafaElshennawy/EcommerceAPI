using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand (Guid ProductId):IRequest<ErrorOr<Unit>>;
}
