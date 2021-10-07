using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using AuthApi.Models;

namespace AuthApi.Repository
{
    public class AuthRepository
    {
        private readonly string _sqlDataSource;
        public AuthRepository(IConfiguration configuration)
        {
            _sqlDataSource = configuration.GetConnectionString("ProductAppCon");
        }

        public Account GetAccount(Login request)
        {
            var account = new Account();

            using (SqlConnection myCon = new SqlConnection(_sqlDataSource))
            using (SqlCommand command = new SqlCommand("spUsers_GetUser", myCon))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@pEmail", SqlDbType.NVarChar, 100).Value = request.Email;
                command.Parameters.Add("@pPassword", SqlDbType.NVarChar, 100).Value = request.Password;

                myCon.Open();
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    account = Account.MapFrom(reader);
            }
            return account.ToDomainModel();
        }
    }
}
