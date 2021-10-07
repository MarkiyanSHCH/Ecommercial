using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using AuthApi.Models;



namespace AuthApi.Repository
{
    public class AuthRepository
    {
        public Account GetAccount(IConfiguration _configuration, Login request)
        {
            string sqlDataSource = _configuration.GetConnectionString("ProductAppCon");
            var account = new Account();

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            using (SqlCommand command = new SqlCommand("spUsers_GetUser", myCon))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@pEmail", SqlDbType.NVarChar, 100).Value = request.Email;
                command.Parameters.Add("@pPassword", SqlDbType.NVarChar, 100).Value = request.Password;

                myCon.Open();
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    account = Account.MapFrom(reader);

                myCon.Close();

            }
            return account.ToDomainModel();
        }
    }
}
