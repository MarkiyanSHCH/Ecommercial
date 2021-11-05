using System.Data;
using System.Data.SqlClient;
using System;

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
            => this._sqlDataSource = configuration.GetConnectionString("ProductAppCon");

        public Account GetAccount(string Email)
        {
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            using (SqlCommand command = new SqlCommand("spUsers_Get", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters
                    .Add("@Email", SqlDbType.NVarChar, 100)
                    .Value = Email;

                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return AccountDTO.MapFrom(reader).ToDomainModel();
            }
            return null;
        }

        public int AddUser(
           string name,
           string email,
           string password)
        {
            try
            {
                using var connection = new SqlConnection(_sqlDataSource);
                using var command = new SqlCommand("spUsers_Add", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("@Name", SqlDbType.VarChar, 40).Value = name;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = password;

                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}