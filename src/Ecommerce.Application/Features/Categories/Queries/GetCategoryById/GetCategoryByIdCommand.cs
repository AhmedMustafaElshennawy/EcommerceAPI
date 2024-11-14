using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdCommand(Guid categoryId
        ) : IRequest<ErrorOr<Category>>;

}
