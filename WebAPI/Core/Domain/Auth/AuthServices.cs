using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Domain.Models;
using Core.Handlers.Hashing;
using Core.Domain.Token;
using Core.Domain.Token.Models;

namespace Core.Domain.Auth
{
    public class AuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly HashingService _hashingService;
        private readonly TokenServices _tokenServices;

        public AuthServices( 
            IAuthRepository authRepository,
            HashingService hashingService,
            TokenServices tokenServices)
        {
            this._authRepository = authRepository;
            this._hashingService = hashingService;
            this._tokenServices = tokenServices;
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
                return new Account
                {
                    Id = this._authRepository.AddUser(name, email, this._hashingService.Hash(password)),
                    Name = name,
                    Email = email
                };

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

        public string GetUserEmail(ClaimsPrincipal userPrincipal)
            => Convert.ToString(userPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email).Value);

        public Tokens GenerateToken(Account user)
            => this._tokenServices.GenerateToken(user);

        public async Task LogoutAsync(string authHeader)
            => await this._tokenServices.DeactivateTokenAsync(authHeader.Split(" ").Last());
    }
}