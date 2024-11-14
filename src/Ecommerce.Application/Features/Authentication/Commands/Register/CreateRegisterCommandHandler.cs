using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.Register
{
    public record RegisterResult(ApplicationUser User, string Token);
    public class CreateRegisterCommandHandler : IRequestHandler<CreateRegisterCommand, ErrorOr<RegisterResult>>
    {
        private readonly IBaseRepository<ApplicationUser> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public CreateRegisterCommandHandler(
            IBaseRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<ErrorOr<RegisterResult>> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(user, request.Password);

            if (!creationResult.Succeeded)
                return Error.Failure("Failed at creating user: " + string.Join(", ", creationResult.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(request.Role));
                if (!roleResult.Succeeded)
                    return Error.Failure("Failed to create role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addToRoleResult.Succeeded)
                return Error.Failure("Failed to add user to role: " + string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));

            var token = await _tokenService.GenerateTokenAsync(user, request.Role);
            await _unitOfWork.CompleteAsync();

            var response = new RegisterResult(user, token);
            return response;
        }
    }
}