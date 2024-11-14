using Ecommerce.Domain.identity;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.Register
{

    public record CreateRegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string PasswordConfirmation,
    string PhoneNumber,
    string Role) : IRequest<ErrorOr<RegisterResult>>;
}