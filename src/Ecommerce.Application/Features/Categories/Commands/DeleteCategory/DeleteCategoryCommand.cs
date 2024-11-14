using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid CategoryId): IRequest<ErrorOr<Unit>>;
}
