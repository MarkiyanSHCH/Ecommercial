using System;
using System.Linq;
using System.Security.Cryptography;

using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

namespace Core.Handlers.Hashing
{ 
    public class HashingService
    {
        private readonly IHashingSettings _settings;
        private readonly ILogger _logger;

        public HashingService(IHashingSettings settings, ILogger logger)
            => (this._settings, this._logger) = (settings, logger);

        public string Hash(string input)
        {
            try
            {
                using var hashAlgorithm = new Rfc2898DeriveBytes(
                    input,
                    this._settings.SaltSizeInBytes,
                    this._settings.IterationsCount,
                    new HashAlgorithmName(this._settings.HashAlgorithmName));

                string salt = Convert.ToBase64String(hashAlgorithm.Salt);

                byte[] keyBytes = hashAlgorithm.GetBytes(this._settings.HashSizeInBytes);
                string key = Convert.ToBase64String(keyBytes);

                string hash = HashFormatter.Build(new HashContext
                {
                    HashAlgorithm = new HashAlgorithmName(this._settings.HashAlgorithmName),

                    SaltSizeInBytes = this._settings.SaltSizeInBytes,
                    HashSizeInBytes = this._settings.HashSizeInBytes,
                    Iterations = this._settings.IterationsCount,
                    Salt = salt,
                    Key = key
                });

                return hash;
            }
            catch (Exception ex)
            {
                this._logger.Error(
                    message: "Failed to hash provided input.",
                    scope: ApplicationScope.Hashing,
                    exception: ex);

                return null;
            }
        }

        public bool Check(string hash, string input)
        {
            try
            {
                HashContext hashContext = HashFormatter.Parse(hash);

                byte[] hashedSalt = Convert.FromBase64String(hashContext.Salt);
                byte[] hashedKey = Convert.FromBase64String(hashContext.Key);

                using var hashAlgorithm = new Rfc2898DeriveBytes(
                  input,
                  hashedSalt,
                  hashContext.Iterations,
                  hashContext.HashAlgorithm);

                byte[] inputKey = hashAlgorithm.GetBytes(hashContext.HashSizeInBytes);

                return inputKey.SequenceEqual(hashedKey);
            }
            catch (Exception ex)
            {
                this._logger.Error(
                    message: "Failed to check provided hash and input.",
                    scope: ApplicationScope.Hashing,
                    exception: ex);

                return false;
            }
        }
    }
}