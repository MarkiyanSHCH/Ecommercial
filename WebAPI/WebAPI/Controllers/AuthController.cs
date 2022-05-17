using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Core.Domain.Auth;
using Core.Domain.Token.Models;

using Domain.Models;
using WebAPI.Models.Api.Auth;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
            => this._authServices = authServices;

        [HttpPost("login")]
        public IActionResult Login([FromBody] PostLoginRequest request)
        {
            Account user = this._authServices.GetByEmail(request.Email, request.Password);
            if (user == null)
                return BadRequest();

            Tokens tokens = this._authServices.GenerateToken(user);
            if (tokens != null)
                return Ok(
                    new
                    {
                        access_token = tokens.Token,
                        refresh_token = tokens.RefreshToken
                    });

            return Unauthorized();
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] PostRegistrationAccountRequest request)
        {
            Account newUser = this._authServices.AddUser(request.Name, request.Email, request.Password);
            if (newUser == null)
                return BadRequest();

            Tokens tokens = this._authServices.GenerateToken(newUser);
            if (tokens != null)
                return Ok(
                    new
                    {
                        access_token = tokens.Token,
                        refresh_token = tokens.RefreshToken
                    });

            return BadRequest();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader] string authorization)
        {
            if (base.HttpContext.User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(authorization))
                await this._authServices.LogoutAsync(authorization);

            return NoContent();
        }
    }
}