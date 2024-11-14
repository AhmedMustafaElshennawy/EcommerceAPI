using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.review;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.review.Queries.GetReviewById
{
    public class GetReviewQueryHandler : IRequestHandler<DeleteReviewCommand, ErrorOr<Review>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetReviewQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        } 
        public async Task<ErrorOr<Review>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Error.NotFound("User.NotFound", "User not found.");

            var review = await _unitOfWork.Reviews.GetEntityByIdAsync(request.ReviewId);
            if (review is null)
                return Error.NotFound(description:"No Review with this Id you Enter it");

            return review;
        }
    }
}
