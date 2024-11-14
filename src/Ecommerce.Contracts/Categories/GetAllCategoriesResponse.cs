using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Categories
{
    public record GetAllCategoriesResponse(IEnumerable<CreateCategoryResponse> Response);
}
