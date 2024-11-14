using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery (): IRequest<ErrorOr<IEnumerable<Category>>>;

}
