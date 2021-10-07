using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using AuthApi.Models;
using AuthApi.Services;

using AuthCommon;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthServices _authServices;

        public AuthController(IConfiguration configuration, IOptions<AuthOptions> authOptions)
        {
            _configuration = configuration;
            _authServices = new AuthServices(authOptions);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            var user = _authServices.GetUser(_configuration, request);

            if (user != null)
            {
                var token = _authServices.GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }
            return Unauthorized();
        }
    }
}
