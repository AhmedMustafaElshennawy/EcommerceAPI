using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.review.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator:AbstractValidator<UpdateReviewCommand>
    {
        private bool BeAValidGuid(Guid productId) => productId != Guid.Empty;
        public UpdateReviewCommandValidator() 
        {
            RuleFor(command => command.ProductId)
               .NotEmpty().WithMessage("ProductId must not be empty.")
               .Must(BeAValidGuid).WithMessage("ProductId must be a valid GUID.");

            RuleFor(command => command.Rating)
                .GreaterThan(0).WithMessage("Rating must be greater than ZERO")
                .LessThan(6).WithMessage("Rating can not be greater than 5");

            RuleFor(command => command.Comment)
                .MinimumLength(10).WithMessage("Comment can not be less than 10 charachters")
                .MaximumLength(250).WithMessage("Comment can not be greater than 250 charachters");
        }
    }
}
