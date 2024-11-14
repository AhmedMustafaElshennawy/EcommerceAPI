using Ecommerce.Application.Features.Categories.Commands.UpdateCategory;
using FluentValidation;
using System;

namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category ID must be a valid non-empty GUID.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required.")
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}