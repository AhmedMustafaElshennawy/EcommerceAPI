using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.review;
using Ecommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Interfaces
{
    public class ReviewRepository :BaseRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) : base(context) => _context = context;
        public Task DeleteReview(Review review)
        {
            var result = _context.Reviews.Remove(review);
            return Task.CompletedTask;
        }

        public async Task<Review> GetReviewById(Guid id,CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.SingleOrDefaultAsync(X => X.ReviewId == id, cancellationToken);
            return review;
        }
    }
}
