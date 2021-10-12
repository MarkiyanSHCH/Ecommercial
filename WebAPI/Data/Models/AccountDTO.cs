using System.Data;
using System.Data.SqlClient;

using Domain.Models;

namespace Data.Models
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static AccountDTO MapFrom(SqlDataReader reader)
            => new AccountDTO
            {
                Id = reader.GetInt32(nameof(Id)),
                Name = reader.GetString(nameof(Name)),
                Email = reader.GetString(nameof(Email)),
                Password = reader.GetString(nameof(Password))
            };

        public Account ToDomainModel()
            => new Account
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
    }
}