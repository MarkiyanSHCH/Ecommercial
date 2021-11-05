using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Core.Services.Hashing
{
    internal static class HashFormatter
    {
        private static Dictionary<string, HashAlgorithmName> _algorithmKeys
            = new Dictionary<string, HashAlgorithmName>
            {
                ["alg1"] = HashAlgorithmName.SHA256
            };

        public static string Build(HashContext context)
        {
            string algorithmKey = _algorithmKeys.First(pair => pair.Value == context.HashAlgorithm).Key;
            string metadata = string.Format(
                "{0}-{1}-{2}", 
                algorithmKey, 
                context.SaltSizeInBytes, 
                context.HashSizeInBytes);
            return string.Format("{0}.{1}.{2}.{3}", metadata, context.Iterations, context.Salt, context.Key);
        }

        public static HashContext Parse(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return null;

            string[] hashParts = hash.Split('.');
            if (hashParts.Length == 4)
            {
                string[] metaParts = hashParts[0].Split('-');
                if (metaParts.Length != 3)
                    return null;

                return new HashContext
                {
                    HashAlgorithm = _algorithmKeys.GetValueOrDefault(metaParts[0]),
                    SaltSizeInBytes = int.Parse(metaParts[1]),
                    HashSizeInBytes = int.Parse(metaParts[2]),
                    Iterations = int.Parse(hashParts[1]),
                    Salt = hashParts[2],
                    Key = hashParts[3],
                    NeedsUpgrade = false
                };
            }
            return null;
        }
    }
}
