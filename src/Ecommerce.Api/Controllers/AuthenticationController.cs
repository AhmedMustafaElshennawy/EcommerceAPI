using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Authentication.Commands;
using Ecommerce.Application.Features.Authentication.Commands.Logout;
using Ecommerce.Application.Features.Authentication.Commands.Register;
using Ecommerce.Application.Features.Authentication.Queries.Login;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Contracts.Card_CardItems;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.product;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly ITokenService _tokenService;
        public AuthenticationController(ISender mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            var defaultUserRole = "shopper";
            var command = new CreateRegisterCommand(
                request.firstName,
                request.lastName,
                request.userName,
                request.email,
                request.password,
                request.passwordComfirmation,
                request.phoneNumber,
                defaultUserRole);

            var result = await _mediator.Send(command);

            var response = result.Match(
                success =>
                {
                    
                    var registerResponse = new RegisterResponse(
                        success.User.Id,
                        success.User.FirstName,
                        success.User.LastName,
                        success.User.UserName!,
                        success.User.Email!,
                        success.User.PhoneNumber!,
                        success.Token
                    );
                    return Ok(registerResponse);
                },
                Problem
            );

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            var command = new CreateLoginCommand(
                 request.email,
                 request.password);

            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(new LoginResponse(
                    result.Value.User.Id,
                    result.Value.User.FirstName,
                    result.Value.User.LastName,
                    result.Value.User.UserName!,
                    result.Value.User.Email!,
                    result.Value.User.PhoneNumber!,
                    result.Value.Role,
                    result.Value.Token
                    )),
                Problem);

            return response;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var command = new CreateLogoutCommand();
            var result = await _mediator.Send(command);

            var response = result.Match(
                success => Ok(new LogoutResponse(message: "User logged out successfully.")),
                Problem);

            return response;
        }
    }
}