using System;
using System.Linq;
using System.Security.Cryptography;

using Core.Services.Hashing;

namespace Core.Services
{
    public class HashingService
    {
        private readonly IHashingSettings _settings;

        public HashingService(IHashingSettings settings)
            => this._settings = settings;

        public string Hash(string input)
        {
            try
            {
                using var hashAlgorithm = new Rfc2898DeriveBytes(
                    input,
                    this._settings.SaltSizeInBytes,
                    this._settings.IterationsCount,
                    (HashAlgorithmName)Enum.Parse(
                        typeof(HashAlgorithmName),
                        this._settings.HashAlgorithmName));

                string salt = Convert.ToBase64String(hashAlgorithm.Salt);

                byte[] keyBytes = hashAlgorithm.GetBytes(this._settings.HashSizeInBytes);
                string key = Convert.ToBase64String(keyBytes);

                string hash = HashFormatter.Build(new HashContext
                {
                    HashAlgorithm = (HashAlgorithmName)Enum.Parse(
                        typeof(HashAlgorithmName),
                        this._settings.HashAlgorithmName),

                    SaltSizeInBytes = this._settings.SaltSizeInBytes,
                    HashSizeInBytes = this._settings.HashSizeInBytes,
                    Iterations = this._settings.IterationsCount,
                    Salt = salt,
                    Key = key
                });

                return hash;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Check(string hash, string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hash) || string.IsNullOrWhiteSpace(input))
                    throw new ArgumentNullException();

                HashContext hashContext = HashFormatter.Parse(hash);
                if (hashContext == null)
                    throw new ArgumentNullException();

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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
