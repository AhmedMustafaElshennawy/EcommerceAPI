using Ecommerce.Application.Features.Categories.Queries.GetCategoryById;
using FluentValidation;
using System;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdCommandValidator : AbstractValidator<GetCategoryByIdCommand>
    {
        public GetCategoryByIdCommandValidator()
        {
            RuleFor(x => x.categoryId)
                .NotEqual(Guid.Empty).WithMessage("Category ID must be a valid non-empty GUID.");
        }
    }
}