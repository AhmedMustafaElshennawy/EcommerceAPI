using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.product
{
    public static class ProductErrors
    {
        public static readonly Error NotFoundWithThisId = Error.NotFound(
            code: "Product.NotFound",
            description: "No Product with this Id");

        public static readonly Error NoProductsFound = Error.NotFound(
            code: "Product.NotFound",
            description: "No Products are found.");
    }
}
