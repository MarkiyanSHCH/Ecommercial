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
        private readonly HashingService _hashingService;

        public AuthServices(IOptions<AuthOptions> authOptions, IAuthRepository authRepository, HashingService hashingService)
            => (this._authOptions, this._authRepository, this._hashingService) = (authOptions, authRepository, hashingService);

        public Account GetAccount(string inputEmail, string inputPassword)
        {
            if (string.IsNullOrWhiteSpace(inputEmail) || string.IsNullOrWhiteSpace(inputPassword))
                return null;

            Account user = this._authRepository.GetAccount(inputEmail);
            return user != null && _hashingService.Check(user.Password, inputPassword) ? user : null;
        }

        public Account AddUser(string name, string email, string password)
        {
            Account user = this._authRepository.GetAccount(email);

            if (user == null) {
                string hashResult = this._hashingService.Hash(password);
                return new Account
                {
                    Id = this._authRepository.AddUser(name, email, hashResult),
                    Name = name,
                    Email = email,
                    Password = hashResult
                };
            }
            else return null;
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