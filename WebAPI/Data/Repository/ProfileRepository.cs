using System;
using System.Data;
using System.Data.SqlClient;

using Core.Domain.Profiles;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public ProfileRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public Account GetProfileById(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
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
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to get profile by user id",
                   ApplicationScope.Profile,
                   new { userId },
                   ex);

                return null;
            }
        }

        public bool UpdateName(int userId, string name)
        {
            try
            {
                using var connection = new SqlConnection(this._settings.ConnectionString);
                using var command = new SqlCommand("spUsers_UpdateName", connection)
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
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to update profile's name by user id",
                   ApplicationScope.Profile,
                   new { userId, name },
                   ex);

                return false;
            }
        }

        public bool UpdatePassword(int userId, string hashPassword)
        {
            try
            {
                using var connection = new SqlConnection(this._settings.ConnectionString);
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
            catch (Exception ex)
            {
                this._logger.Error(
                   "Failed to update profile's password by user id",
                   ApplicationScope.Profile,
                   new { userId, hashPassword },
                   ex);

                return false;
            }
        }
    }
}