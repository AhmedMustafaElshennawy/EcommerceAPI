﻿using Ecommerce.Application.Features.Categories.Commands.CreateCategory;
using FluentValidation;

namespace Ecommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required.")
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}