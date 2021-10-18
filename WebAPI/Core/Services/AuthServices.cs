using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class AuthServices
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IAuthRepository _authRepository;

        public AuthServices(IOptions<AuthOptions> authOptions, IAuthRepository authRepository)
            => (this._authOptions, this._authRepository) = (authOptions, authRepository);

        public Account GetAccount(string Email, string Password)
        {
            Account user = this._authRepository.GetAccount(Email);
            return user != null || user.Password == Password ? user : null;
        }

        public string GenerateJWT(Account user)
        {
            AuthOptions authParams = _authOptions.Value;
            SymmetricSecurityKey securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}