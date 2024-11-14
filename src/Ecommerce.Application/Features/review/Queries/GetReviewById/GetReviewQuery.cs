using Ecommerce.Domain.review;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.review.Queries.GetReviewById
{
    public record DeleteReviewCommand(Guid ReviewId):IRequest<ErrorOr<Review>>;
}
