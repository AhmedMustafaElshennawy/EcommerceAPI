using Ecommerce.Domain.identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.Logout
{
    public class CreateLogoutCommandHandler : IRequestHandler<CreateLogoutCommand, ErrorOr<Unit>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLogoutCommandHandler(
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateLogoutCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity!.IsAuthenticated)
            {
                return Error.Failure(description: "User is not signed in.");
            }

            await _signInManager.SignOutAsync();
            return Unit.Value;
        }
    }
}