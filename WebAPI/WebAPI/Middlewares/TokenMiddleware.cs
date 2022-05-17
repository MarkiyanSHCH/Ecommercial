using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Core.Domain.Token;

namespace WebAPI.API.Middlewares
{
    public class TokenMiddleware : IMiddleware
    {
        private readonly TokenServices _tokensService;

        public TokenMiddleware(TokenServices tokensService)
            => this._tokensService = tokensService;
        

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(authHeader))
            {
                await next(context);
                return;
            }

            if (!await this._tokensService.IsDeactivatedTokenAsync(authHeader.Split(" ").Last()))
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}