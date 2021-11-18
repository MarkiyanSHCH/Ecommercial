using Domain.Models;

namespace Core.Repository
{
    public interface IProfileRepository
    {
        Account GetProfileById(int userId);

        bool UpdateName(int userId, string name);

        bool UpdatePassword(int userId, string hashPassword);
    }
}