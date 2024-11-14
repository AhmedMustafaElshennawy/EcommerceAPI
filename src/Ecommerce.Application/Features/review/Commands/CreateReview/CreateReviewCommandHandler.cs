using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.product;
using Ecommerce.Domain.review;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.review.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ErrorOr<Review>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateReviewCommandHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<Review>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Error.NotFound("User.NotFound", "User not found.");

            var product = await _unitOfWork.Products.GetEntityByIdAsync(request.ProductId);
            if (product == null)
                return Error.NotFound("Product.NotFound", "Product not found.");

            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                ProductId = request.ProductId,
                ApplicationUserId = userId,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewDate = DateTime.UtcNow
            };

            await _unitOfWork.Reviews.CreateEntityAsync(review);
            await _unitOfWork.CompleteAsync();

            return review;
        }
    }
}
