using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using Data.Models;
using Domain.Models;
using Core.Repository;

namespace Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _sqlDataSource;

        public AuthRepository(IConfiguration configuration)
            => (this._sqlDataSource) = (configuration.GetConnectionString("ProductAppCon"));

        public Account GetAccount(string Email, string Password)
        {
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            using (SqlCommand command = new SqlCommand("spUsers_GetUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@pEmail", SqlDbType.NVarChar, 100).Value = Email;
                command.Parameters.Add("@pPassword", SqlDbType.NVarChar, 100).Value = Password;

                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return AccountDTO.MapFrom(reader).ToDomainModel();
            }
            return null;
        }
    }
}