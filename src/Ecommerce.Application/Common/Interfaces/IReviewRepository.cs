using Ecommerce.Domain.identity;
using Ecommerce.Domain.order;
using Ecommerce.Domain.review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<Review> GetReviewById(Guid id, CancellationToken cancellationToken);
        Task DeleteReview(Review review);
    }
}
