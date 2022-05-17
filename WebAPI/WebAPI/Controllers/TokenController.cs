using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Models.Api.Token;
using Core.Domain.Token.Models;
using Core.Domain.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    { 
        private readonly TokenServices _tokenServices;

        public TokenController(TokenServices tokenServices)
            => this._tokenServices = tokenServices;

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] PostTokenRequest request)
        {
            if (request == null)
                return BadRequest();

            Tokens tokens = await this._tokenServices.RefreshToken(request.Token, request.RefreshToken);
            if(tokens != null)
                return Ok(
                    new
                    {
                        access_token = tokens.Token,
                        refresh_token = tokens.RefreshToken
                    });

            return BadRequest();
        }      
    }
}