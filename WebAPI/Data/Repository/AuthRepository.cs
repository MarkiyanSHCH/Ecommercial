using System;
using System.Data;
using System.Data.SqlClient;

using Core.Domain.Auth;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using Data.Models;
using Domain.Models;

namespace Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbSettings _settings;
        private readonly ILogger _logger;

        public AuthRepository(IDbSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public Account GetByEmail(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._settings.ConnectionString))
                using (SqlCommand command = new SqlCommand("spUsers_Get", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters
                        .Add("@Email", SqlDbType.NVarChar, 100)
                        .Value = email;

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
                   "Failed to get account by Email.",
                   ApplicationScope.Auth,
                   email,
                   ex);

                return null;
            }
        }

        public int AddUser(
           string name,
           string email,
           string password)
        {
            try
            {
                using var connection = new SqlConnection(this._settings.ConnectionString);
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
                this._logger.Error(
                   "Failed to add account",
                   ApplicationScope.Auth,
                   new { name, email, password },
                   ex);

                return 0;
            }
        }
    }
}