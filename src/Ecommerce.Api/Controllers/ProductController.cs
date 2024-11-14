using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries.GetAllProducts;
using Ecommerce.Application.Features.Products.Queries.GetById;
using Ecommerce.Application.Products.Commands.CreateProduct;
using Ecommerce.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ApiController
    {
        private readonly ISender _mediator;
        public ProductController(ISender mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.PictureUrl,
                request.Price,
                request.Discount,
                request.CategoryId);

            var result = await _mediator.Send(command);

            var response = result.Match(success => Ok(
                new CreateProductResponse(
                    result.Value.Id,
                    result.Value.Name,
                    result.Value.Description,
                    result.Value.PictureUrl,
                    result.Value.Price,
                    result.Value.Discount,
                    result.Value.CreatedOn,
                    result.Value.CategoryId)),
                    Problem);

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetProductsQuery();

            var result = await _mediator.Send(query);

            var response = result.Match(
                success => Ok(success.Select(product => new GetAllProductResponse(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.PictureUrl,
                    product.Price,
                    product.Discount,
                    product.CreatedOn,
                    product.CategoryId))),
                    Problem);

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] GetProductByIdRequest request)
        {
            var query = new GetProductByIdQuery(request.productId);
            var result = await _mediator.Send(query);

            var response = result.Match(
                product => Ok(new GetProductByIdResponse(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.PictureUrl,
                    product.Price,
                    product.Discount,
                    product.CreatedOn,
                    product.CategoryId)),
                    Problem);

            return response;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductRequest request)
        {
            var command = new DeleteProductCommand(request.productId);
            var result = await _mediator.Send(command);

            var response = result.Match(
                 success => NoContent(),
                 Problem);

            return response;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand(
                request.productId,
                request.Name,
                request.Description,
                request.PictureUrl,
                request.Price,
                request.Discount,
                request.CategoryId);

            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(new UpdateProductResponse(
                result.Value.Id,
                result.Value.Name,
                result.Value.Description,
                result.Value.PictureUrl,
                result.Value.Price,
                result.Value.Discount,
                result.Value.CategoryId)),
                Problem);

            return response;
        }   
    }
}