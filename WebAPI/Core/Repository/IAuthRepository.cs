using Domain.Models;

namespace Core.Repository
{
    public interface IAuthRepository
    {
        Account GetAccount(string Email, string Password);
    }
}