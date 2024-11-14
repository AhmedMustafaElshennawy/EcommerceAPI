using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Categories
{
    public record CreateCategoryResponse(
        Guid Id, 
        string Name, 
        string Description, 
        DateTime CreatedOn);
}
