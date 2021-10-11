using System.Data;
using System.Data.SqlClient;

namespace AuthApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static Account MapFrom(SqlDataReader reader)
          => new Account
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
