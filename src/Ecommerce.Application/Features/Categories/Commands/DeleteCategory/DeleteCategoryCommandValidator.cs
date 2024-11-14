using Ecommerce.Application.Features.Categories.Commands.DeleteCategory;
using FluentValidation;
using System;

namespace Ecommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category ID must be a valid non-empty GUID.");
        }
    }
}