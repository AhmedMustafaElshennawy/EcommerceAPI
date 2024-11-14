
using FluentValidation;

namespace Ecommerce.Application.Features.Authentication.Queries.Login
{
    public class CreateLoginCommandValidator : AbstractValidator<CreateLoginCommand>
    {
        public CreateLoginCommandValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}