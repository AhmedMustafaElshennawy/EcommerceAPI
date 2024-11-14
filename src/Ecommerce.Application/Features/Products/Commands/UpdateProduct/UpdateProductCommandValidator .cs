using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png" };
        private bool BeAValidGuid(Guid productId) => productId != Guid.Empty;
        private bool BeAValidImage(IFormFile pictureUrl)
        {
            var extension = Path.GetExtension(pictureUrl.FileName).ToLowerInvariant();
            var result = !string.IsNullOrEmpty(extension) && _permittedExtensions.Contains(extension);
            return result;
        }
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.productId)
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .Must(BeAValidGuid).WithMessage("ProductId must be a valid GUID.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product name must not be empty.")
                .MinimumLength(5).WithMessage("Product name must not be less than 5 characters.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Product description must not be empty.")
                .MinimumLength(5).WithMessage("Product description must not be less than 5 characters.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(command => command.Discount)
                .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");

            RuleFor(command => command.CategoryId)
                .NotEmpty().WithMessage("CategoryId must not be empty.");

            RuleFor(command => command.PictureUrl)
                .Must(BeAValidImage).WithMessage("PictureUrl must be a valid image file.")
                .When(command => command.PictureUrl != null); // Only validate  ==> if PictureUrl is provided
        }
    }
}