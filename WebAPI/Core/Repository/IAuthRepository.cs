using Domain.Models;

namespace Core.Repository
{
    public interface IAuthRepository
    {
        Account GetByEmail(string Email);
        int AddUser(string name, string email, string password);
    }
}