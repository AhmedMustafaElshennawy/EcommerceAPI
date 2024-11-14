using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace Ecommerce.Domain.catgory
{
    public static class CategoryErrors
    {
        public static readonly Error NotFoundWithThisId = Error.NotFound( 
            code: "Category.NotFound",
            description: "No category with this Id");

        public static readonly Error NoCategoriesFound = Error.NotFound(
            code: "Category.NotFound",
            description: "No categories are found.");
    }
}
