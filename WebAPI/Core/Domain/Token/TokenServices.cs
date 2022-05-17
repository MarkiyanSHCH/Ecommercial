using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Caching.Distributed;

using Domain.Models;
using Core.Domain.Token.Models;
using Core.Domain.Profiles;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

namespace Core.Domain.Token
{
    public class TokenServices
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly ProfileServices _profileServices;
        private readonly ILogger _logger;
        private readonly IDistributedCache _cache;

        public TokenServices(
            IOptions<AuthOptions> authOptions,
            ProfileServices profileServices,
            ILogger logger,
            IDistributedCache cache)
        {
            this._authOptions = authOptions;
            this._profileServices = profileServices;
            this._logger = logger;
            this._cache = cache;
        }

        public async Task<Tokens> RefreshToken(string token, string refreshToken)
        {
            try
            {
                if (token == null || refreshToken == null)
                    throw new ArgumentException("token or refreshToken is empty");

                if (await this.IsDeactivatedTokenAsync(refreshToken))
                    throw new ArgumentException("refreshToken is inactive");

                // Parse Tokens
                JwtSecurityToken validatedToken = this.ParseToken(token);
                JwtSecurityToken validatedRefreshToken = this.ParseToken(refreshToken);

                if (validatedToken == null || validatedRefreshToken == null)
                    throw new ArgumentException("(validatedToken) or (validatedRefreshToken) is empty");

                // Get Expiration Date From Claims
                DateTimeOffset expToken = DateTimeOffset.FromUnixTimeSeconds(long.Parse(this.GetClaim(validatedToken, JwtRegisteredClaimNames.Exp)));
                DateTimeOffset expRefreshToken = DateTimeOffset.FromUnixTimeSeconds(long.Parse(this.GetClaim(validatedRefreshToken, JwtRegisteredClaimNames.Exp)));

                if (expToken > DateTimeOffset.UtcNow || expRefreshToken < DateTimeOffset.UtcNow)
                    return null;

                if (!this.ValidateTokem(validatedToken, validatedRefreshToken))
                    return null;

                await this.DeactivateTokenAsync(refreshToken);

                return
                    this.GenerateToken(
                        this._profileServices
                                .GetProfileById(int.Parse(
                                    this.GetClaim(validatedToken, JwtRegisteredClaimNames.Sub)
                                    ))
                        );
            }
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to refresh token",
                   ApplicationScope.Token,
                   new { token, refreshToken },
                   ex);

                return null;
            }
        }

        public Tokens GenerateToken(Account user)
        {
            try
            {
                AuthOptions authParams = this._authOptions.Value;

                Guid tokenId = Guid.NewGuid();
                Guid refreshTokenId = Guid.NewGuid();

                DateTime expiresToken = DateTime.UtcNow.Add(authParams.TokenLifetime);
                DateTime expiresRefreshToken = DateTime.UtcNow.Add(authParams.RefreshTokenLifetime);

                var claimsToken = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, refreshTokenId.ToString())
                };

                var claimsRefreshToken = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Jti, refreshTokenId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, tokenId.ToString())
                };

                return
                    new Tokens
                    {
                        Token = this.Generate(claimsToken, expiresToken),
                        RefreshToken = this.Generate(claimsRefreshToken, expiresRefreshToken)
                    };
            }
            catch (Exception ex)
            {
                this._logger.Error(
                  "Failed to generate token",
                  ApplicationScope.Token,
                  user,
                  ex);

                return null;
            }
        }

        public async Task<bool> IsDeactivatedTokenAsync(string tokenString)
            => await this._cache.GetStringAsync(this.GetCacheKey(this.ParseToken(tokenString))) != null;

        public async Task DeactivateTokenAsync(string token)
            => await this._cache.SetStringAsync(
                key: this.GetCacheKey(this.ParseToken(token)),
                value: string.Empty);

        private string Generate(IEnumerable<Claim> claims, DateTime expiresToken)
        {
            AuthOptions authParams = this._authOptions.Value;
            SymmetricSecurityKey securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(authParams.Issuer,
              authParams.Audience,
              claims,
              expires: expiresToken,
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken ParseToken(string tokenString)
            => new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

        private bool ValidateTokem(JwtSecurityToken validatedToken, JwtSecurityToken validatedRefreshToken)
           => this.GetClaim(validatedToken, JwtRegisteredClaimNames.Jti) == this.GetClaim(validatedRefreshToken, JwtRegisteredClaimNames.Sid)
           && this.GetClaim(validatedToken, JwtRegisteredClaimNames.Sid) == this.GetClaim(validatedRefreshToken, JwtRegisteredClaimNames.Jti);

        private string GetClaim(JwtSecurityToken token, string claimType)
            => token.Claims.Single(claim => claim.Type == claimType).Value;

        private string GetCacheKey(JwtSecurityToken token)
           => $"tokens:{token.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti)?.Value ?? string.Empty}:deactivated";
    }
}
