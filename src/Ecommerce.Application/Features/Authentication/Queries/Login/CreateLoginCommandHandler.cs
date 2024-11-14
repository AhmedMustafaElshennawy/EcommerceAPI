using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Queries.Login
{
    public record LoginResult(ApplicationUser User, string Role, string Token);

    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, ErrorOr<LoginResult>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public CreateLoginCommandHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ErrorOr<LoginResult>> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
                return Error.Forbidden("No user found with this email or password.");

            var result = await _signInManager.PasswordSignInAsync(user, request.password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Error.Failure("No user found with this email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "shopper"; // Default to "shopper" if the user has non
            var token = await _tokenService.GenerateTokenAsync(user, userRole);
                
            var response = new LoginResult(user, userRole, token);
            return response;
        }
    }
}