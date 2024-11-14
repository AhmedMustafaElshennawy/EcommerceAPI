using FluentValidation;
using System;

namespace Ecommerce.Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        private bool BeAValidGuid(Guid productId) => productId != Guid.Empty;
        public GetProductByIdQueryValidator()
        {
            RuleFor(query => query.ProductId)
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .Must(BeAValidGuid).WithMessage("ProductId must be a valid GUID.");
        }
    }
}