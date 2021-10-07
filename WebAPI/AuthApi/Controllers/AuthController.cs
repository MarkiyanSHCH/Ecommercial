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
            _authServices = new AuthServices(configuration, authOptions);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            Account user = _authServices.GetAccount(request);

            if (user != null)
                return Ok(new { access_token = _authServices.GenerateJWT(user) });

            return Unauthorized();
        }
    }
}
