using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.order
{
    public class OrderErrors
    {
        public static readonly Error NotFoundWithThisId = Error.NotFound(
            code: "Order.NotFound",
            description: "No Order with this Id");
    }
}
