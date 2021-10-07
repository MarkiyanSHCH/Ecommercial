using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using AuthApi.Models;
using AuthApi.Repository;

using AuthCommon;

namespace AuthApi.Services
{
    public class AuthServices
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly AuthRepository _authRepository;
        public AuthServices(IOptions<AuthOptions> authOptions)
        {
            _authRepository = new AuthRepository();
            _authOptions = authOptions;
        }

        public Account GetUser(IConfiguration _configuration, Login request)
            => _authRepository.GetAccount(_configuration,request);

        public string GenerateJWT(Account user)
        {
            var authParams = _authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
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
