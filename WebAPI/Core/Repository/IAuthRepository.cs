using Domain.Models;

namespace Core.Repository
{
    public interface IAuthRepository
    {
        Account GetAccount(string Email);
        int AddUser(string name, string email, string password);
    }
}