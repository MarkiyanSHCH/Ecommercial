using AuthApi.Models;
using AuthApi.Repository;

using AuthCommon;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AuthApi.Services
{
    public class AuthServices
    {
        public readonly DataBase _database;
        public AuthServices()
        {
            _database = new DataBase();
        }

        public Account GetUser(IConfiguration _configuration, Login request)
        {
            string query = @"Exec spUsers_GetUser @pEmail = '" + request.Email + "', @pPassword = '" + request.Password + "'";


            return _database.ReadDatabase(_configuration, query).AsEnumerable().Select(row => new Account
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                Email = Convert.ToString(row["Email"]),
                Password = Convert.ToString(row["Password"]),
            }).First();

        }

        public string GenerateJWT(Account user, IOptions<AuthOptions> _options)
        {
            var authParams = _options.Value;
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
