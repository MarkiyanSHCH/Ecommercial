using Domain.Models;

namespace Core.Domain.Auth
{
    public interface IAuthRepository
    {
        Account GetByEmail(string email);
        int AddUser(string name, string email, string password);
    }
}