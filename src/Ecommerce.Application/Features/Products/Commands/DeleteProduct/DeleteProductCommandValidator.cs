using FluentValidation;

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private bool BeAValidGuid(Guid productId) => productId != Guid.Empty;
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.ProductId)
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .Must(BeAValidGuid).WithMessage("ProductId must be a valid GUID.");
        }
    }
}