using Domain.Models;

namespace Core.Repository
{
    public interface IAuthRepository
    {
        public Account GetAccount(string Email, string Password);
    }
}