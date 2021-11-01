using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Core.Services.Hashing;

namespace Core.Services
{
    public class HashingService
    {
        private readonly IHashingSettings _settings;
        private readonly HashFormatter _hashFormatter;
        private readonly Dictionary<string, ParametersPolicy> _supportedPolicies
            = new Dictionary<string, ParametersPolicy>
            {
                [HashAlgorithmName.SHA256.Name] = new ParametersPolicy
                {
                    HashAlgorithm = HashAlgorithmName.SHA256,
                    MinInputCharactersSize = 6,
                    MinSaltSizeInBytes = 16,
                    MinHashSizeInBytes = 32,
                    MinIterationsCount = 10000
                }
            };

        public HashingService(IHashingSettings settings)
        {
            this._settings = settings;
            this._hashFormatter = new HashFormatter();
        }

        public string Hash(string input)
        {
            try
            {
                ParametersPolicy policy = this._supportedPolicies.GetValueOrDefault(this._settings.HashAlgorithmName);
                if (policy == null) return null;

                using var hashAlgorithm = new Rfc2898DeriveBytes(
                    password: input,
                    saltSize: this._settings.SaltSizeInBytes,
                    iterations: this._settings.IterationsCount,
                    hashAlgorithm: policy.HashAlgorithm);

                string salt = Convert.ToBase64String(hashAlgorithm.Salt);

                byte[] keyBytes = hashAlgorithm.GetBytes(this._settings.HashSizeInBytes);
                string key = Convert.ToBase64String(keyBytes);

                string hash = this._hashFormatter.Build(new HashContext
                {
                    HashAlgorithm = policy.HashAlgorithm,
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
                return null;
            }
        }

        public bool Check(string hash, string input)
        {
            try
            {
                HashContext hashContext = this._hashFormatter.Parse(hash);
                if(hashContext == null) return false;

                byte[] hashedSalt = Convert.FromBase64String(hashContext.Salt);
                byte[] hashedKey = Convert.FromBase64String(hashContext.Key);

                using var hashAlgorithm = new Rfc2898DeriveBytes(
                  password: input,
                  salt: hashedSalt,
                  iterations: hashContext.Iterations,
                  hashAlgorithm: hashContext.HashAlgorithm);

                byte[] inputKey = hashAlgorithm.GetBytes(hashContext.HashSizeInBytes);

                bool verified = inputKey.SequenceEqual(hashedKey);

                return verified;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
