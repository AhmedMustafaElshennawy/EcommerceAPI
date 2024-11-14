using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        Guid CategoryId,
        string Name, 
        string Description) : IRequest<ErrorOr<Category>>;
}
