using Ecommerce.Application.Features.review.Commands.CreateReview;
using Ecommerce.Application.Features.review.Commands.UpdateReview;
using Ecommerce.Application.Features.review.Queries.GetReviewById;
using Ecommerce.Contracts.Reviews;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ApiController
    {
        private readonly ISender _mediator;
        public ReviewController(ISender mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreareReviewRequest request)
        {
            var command = request.Adapt<CreateReviewCommand>();
            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(result.Value.Adapt<CreateReviewResponse>()),
                Problem);

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetReview([FromQuery] GetReviewByIdRequest request)
        {
            var query = request.Adapt<DeleteReviewCommand>();
            var result = await _mediator.Send(query);

            var response = result.Match(
                success => Ok(result.Value.Adapt<GetReviewByIdResponse>()),
                Problem);

            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
        {
            var command = request.Adapt<UpdateReviewCommand>();
            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(result.Value.Adapt<UpdateReviewResponse>()), 
                Problem);

            return response;
        }
    }
}