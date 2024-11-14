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
    public record UpdateReviewCommand(
       Guid ReviewId,
       Guid ProductId,
       int Rating,
       string Comment):IRequest<ErrorOr<Review>>;
}
