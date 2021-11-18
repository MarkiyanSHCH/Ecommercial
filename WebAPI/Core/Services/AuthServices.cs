using System;
using System.Linq;
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
        {
            this._authOptions = authOptions;
            this._authRepository = authRepository;
            this._hashingService = hashingService;
        }

        public Account GetByEmail(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            Account user = this._authRepository.GetByEmail(email);
            return user != null && this._hashingService.Check(user.Password, password) ? user : null;
        }

        public Account AddUser(string name, string email, string password)
        {
            Account user = this._authRepository.GetByEmail(email);

            if (user == null)
            {
                string hashResult = this._hashingService.Hash(password);
                return new Account
                {
                    Id = this._authRepository.AddUser(name, email, hashResult),
                    Name = name,
                    Email = email
                };
            }
            return null;
        }

        public bool Authenticate(Account user, string inputPassword)
        {
            if (user == null || string.IsNullOrWhiteSpace(inputPassword))
                return false;

            bool checkResult = this._hashingService.Check(user.Password, inputPassword);
            return checkResult;
        }

        public int GetUserId(ClaimsPrincipal userPrincipal)
            => Convert.ToInt32(userPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public string GenerateJWT(Account user)
        {
            AuthOptions authParams = this._authOptions.Value;
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