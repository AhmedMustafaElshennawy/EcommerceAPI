using Ecommerce.Application.Features.Orders.Commands.CreateOrder;
using Ecommerce.Application.Features.Orders.Commands.DeleteOrder;
using Ecommerce.Application.Features.Orders.Commands.UpdateOrder;
using Ecommerce.Application.Features.Orders.Queries.GetOrderById;
using Ecommerce.Contracts.Order;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : ApiController
    {
        private readonly ISender _mediator;
        public OrderController(ISender mediator) => _mediator = mediator;
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await _mediator.Send(command);
            var response = result.Match(success => Ok(new CreateOrderResponse(result.Value)), Problem);
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdRequest request)
        {
            var query = new GetOrderByIdQuery(request.OrderId);
            var result = await _mediator.Send(query);
            var response = result.Match(success => Ok(success.Adapt<GetOrderByIdResponse>()),Problem);
            return response;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery]DeleteOrderRequest request)
        {
            var command = new DeleteOrderCommand(request.orderId);
            var result = _mediator.Send(command);
            var response = await result.Match(success => NoContent(), Problem);
            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await _mediator.Send(command);
            var response  = result.Match(success => Ok(success.Adapt<UpdateOrderResponse>()),Problem);
            return response;
        }
    }
}