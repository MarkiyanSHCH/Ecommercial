namespace WebAPI.Models.Api.Token
{
    public class PostTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
