using Microsoft.AspNetCore.Mvc;

using Core.Domain.Auth;

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

            if (user != null)
                return Ok(new { access_token = this._authServices.GenerateJWT(user) });

            return Unauthorized();
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] PostRegistrationAccountRequest request)
        {
            Account newUser = this._authServices.AddUser(request.Name, request.Email, request.Password);

            if (newUser != null)
                return Ok(new { access_token = this._authServices.GenerateJWT(newUser) });

            return BadRequest();
        }
    }
}