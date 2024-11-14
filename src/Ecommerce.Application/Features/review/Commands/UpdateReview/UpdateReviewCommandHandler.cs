using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.review;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.review.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ErrorOr<Review>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateReviewCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<ErrorOr<Review>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            
            var review = await _unitOfWork.Reviews.GetEntityByIdAsync(request.ReviewId);
            if (review == null)
                return Error.NotFound("ReviewNotFound", "No review found with the specified ID.");

            review.Rating = request.Rating;
            review.Comment = request.Comment;

            await _unitOfWork.Reviews.UpdateEntityAsync(review);
            await _unitOfWork.CompleteAsync();
            
            return review;
        }
    }
}