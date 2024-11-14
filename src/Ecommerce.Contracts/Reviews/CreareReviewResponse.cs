using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Reviews
{
    public record CreateReviewResponse(
       Guid ReviewId,
       Guid ProductId,
       string ApplicationUserId,
       int Rating,
       string Comment,
       DateTime ReviewDate);

}
