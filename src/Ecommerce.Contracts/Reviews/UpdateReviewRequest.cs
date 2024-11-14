using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Reviews
{
    public record UpdateReviewRequest(
       Guid ReviewId,
       Guid ProductId,
       int Rating,
       string Comment);
}
