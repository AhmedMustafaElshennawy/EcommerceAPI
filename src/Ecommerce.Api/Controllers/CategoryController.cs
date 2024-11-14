using Ecommerce.Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Ecommerce.Application.Features.Categories.Queries.GetCategoryById;
using Ecommerce.Application.Features.Categories.Queries.GetAllCategories;
using Ecommerce.Application.Features.Categories.Commands.DeleteCategory;
using Ecommerce.Application.Features.Categories.Commands.CreateCategory;
using Ecommerce.Application.Features.Categories.Commands.UpdateCategory;
using Ecommerce.Domain.catgory;
using Ecommerce.Contracts.Products;

namespace Ecommerce.Api.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ApiController
    {

        private readonly ISender _mediator;
        public CategoryController(ISender mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm]CreateCategoryRequest request)
        {
            var command = new CreateCategoryCommand(
                request.Name.ToString(),
                request.Description.ToString());

            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(new CreateCategoryResponse(
                result.Value.Id,
                request.Name,
                request.Description,
                result.Value.CrearedOn)),
                Problem);

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetCategoryByIdRequest request)
        {
            var command = new GetCategoryByIdCommand(request.CategoryId);
            var result = await _mediator.Send(command);

            var response = result.Match(
                sucess => Ok(new GetCategoryByIdResponse(
                result.Value.Id,
                result.Value.Name,
                result.Value.Description,
                result.Value.CrearedOn)),
                Problem);

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(Query);

            var response = result.Match(
                success => Ok(success.Select(category => new GetCategoryByIdResponse(
                    category.Id,
                    category.Name,
                    category.Description,
                    category.CrearedOn))),
                    Problem);

            return response;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery]DeleteCategoryRequest request)
        {
            var command = new DeleteCategoryCommand(request.categoryId);
            var result = await _mediator.Send(command);

            var response = result.Match(sucess => NoContent(), Problem);
            return response;  
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromQuery]UpdateCategoryCommand requst)
        {
            var command = new UpdateCategoryCommand(requst.CategoryId, requst.Name, requst.Description);
            var result = await _mediator.Send(command);

            var response = result.Match(sucess =>
                Ok(new UpdateCategoryResponse(
                result.Value.Id,
                result.Value.Name,
                result.Value.Description)),
                Problem);

            return response;
        }
    }
}