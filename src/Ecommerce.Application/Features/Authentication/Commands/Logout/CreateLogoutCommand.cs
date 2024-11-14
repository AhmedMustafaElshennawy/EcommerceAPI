using Ecommerce.Domain.identity;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.Logout
{
    public record CreateLogoutCommand() : IRequest<ErrorOr<Unit>>;
}
