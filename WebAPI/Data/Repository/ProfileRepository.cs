using System.Data.SqlClient;
using System.Data;
using System;

using Microsoft.Extensions.Configuration;

using Data.Models;
using Domain.Models;
using Core.Repository;

namespace Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string _sqlDataSource;

        public ProfileRepository(IConfiguration configuration)
            => this._sqlDataSource = configuration.GetConnectionString("ProductAppCon");

        public Account GetProfileById(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_sqlDataSource))
                using (SqlCommand command = new SqlCommand("spUsers_GetById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@Id", SqlDbType.Int)
                        .Value = userId;

                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return AccountDTO.MapFrom(reader).ToDomainModel();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateName(int userId, string name)
        {
            try
            {
                using var connection = new SqlConnection(_sqlDataSource);
                using var command = new SqlCommand("spUsers_UpdateUserName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters
                    .Add("@UserId", SqlDbType.Int)
                    .Value = userId;
                command.Parameters
                    .Add("@Name", SqlDbType.NVarChar, 40)
                    .Value = name;

                connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePassword(int userId, string hashPassword)
        {
            try
            {
                using var connection = new SqlConnection(_sqlDataSource);
                using var command = new SqlCommand("spUsers_UpdatePassword", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters
                    .Add("@UserId", SqlDbType.Int)
                    .Value = userId;
                command.Parameters
                    .Add("@Password", SqlDbType.NVarChar, 200)
                    .Value = hashPassword;

                connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}