using AuthApi.Models;
using AuthApi.Services;

using AuthCommon;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AuthOptions> _options;
        private readonly AuthServices _services;

        public AuthController(IConfiguration configuration, IOptions<AuthOptions> options)
        {
            _configuration = configuration;
            _services = new AuthServices();
            _options = options;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = _services.GetUser(_configuration, request);

            if (user != null)
            {
                var token = _services.GenerateJWT(user, _options);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }


    }
}
