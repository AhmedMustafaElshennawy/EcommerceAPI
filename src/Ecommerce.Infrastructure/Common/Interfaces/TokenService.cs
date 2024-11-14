using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.identity;
using Ecommerce.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Interfaces
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwt;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService(
        IConfiguration configuration,
        IOptions<JwtSettings> jwt,
        UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _jwt = jwt.Value;
            _userManager = userManager;
        }
        public async Task<string> GenerateTokenAsync(ApplicationUser user, string role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(roleClaims); // Add roles as ==> claims

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials
            );
            var response = new JwtSecurityTokenHandler().WriteToken(token);
            
            return response;
        }
    }
}