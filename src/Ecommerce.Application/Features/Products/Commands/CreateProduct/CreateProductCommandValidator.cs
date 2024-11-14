using Ecommerce.Application.Products.Commands.CreateProduct;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png" };
        private bool BeAValidImageFile(IFormFile file)
        {
            if (file == null) return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var result = !string.IsNullOrEmpty(extension) && _permittedExtensions.Contains(extension);
            return result;
        }
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(5).WithMessage("Product name must not be less than 5 characters.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MinimumLength(5).WithMessage("Product Description must not be less than 5 characters.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
                .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100.");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category ID must be a valid non-empty GUID.");

            RuleFor(x => x.PictureUrl)
                .NotNull().WithMessage("Product image is required.")
                .Must(BeAValidImageFile).WithMessage("Product image must be a valid image file (jpg, JPEG, PNG).");
        }
    }
}