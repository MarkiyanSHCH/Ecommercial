using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Core.Services.Hashing
{
    internal static class HashFormatter
    {
        private const string HASH_FORMAT_TEMPLATE = "{0}.{1}.{2}.{3}";
        private const char HASH_FORMAT_SEPARATOR = '.';
        private const int HASH_FORMAT_PARTS = 4;
        private const int HASH_FORMAT_META_INDEX = 0;
        private const int HASH_FORMAT_ITERATIONS_INDEX = 1;
        private const int HASH_FORMAT_SALT_INDEX = 2;
        private const int HASH_FORMAT_KEY_INDEX = 3;

        private const string META_FORMAT_TEMPLATE = "{0}-{1}-{2}";
        private const char META_FORMAT_SEPARATOR = '-';
        private const int META_FORMAT_PARTS = 3;
        private const int META_FORMAT_ALGORITHM_INDEX = 0;
        private const int META_FORMAT_SALT_LENGTH_INDEX = 1;
        private const int META_FORMAT_KEY_LENGTH_INDEX = 2;

        private static Dictionary<string, HashAlgorithmName> _algorithmKeys
            = new Dictionary<string, HashAlgorithmName>
            {
                ["alg1"] = HashAlgorithmName.SHA256
            };

        public static string Build(HashContext context)
        {
            string algorithmKey = _algorithmKeys.First(pair => pair.Value == context.HashAlgorithm).Key;
            string metadata = string.Format(META_FORMAT_TEMPLATE, algorithmKey, context.SaltSizeInBytes, context.HashSizeInBytes);
            return string.Format(HASH_FORMAT_TEMPLATE, metadata, context.Iterations, context.Salt, context.Key);
        }
        public static HashContext Parse(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return null;

            string[] hashParts = hash.Split(HASH_FORMAT_SEPARATOR);
            if (hashParts.Length == HASH_FORMAT_PARTS)
            {
                string[] metaParts = hashParts[HASH_FORMAT_META_INDEX].Split(META_FORMAT_SEPARATOR);
                if (metaParts.Length != META_FORMAT_PARTS)
                    return null;

                return new HashContext
                {
                    HashAlgorithm = _algorithmKeys.GetValueOrDefault(metaParts[META_FORMAT_ALGORITHM_INDEX]),
                    SaltSizeInBytes = int.Parse(metaParts[META_FORMAT_SALT_LENGTH_INDEX]),
                    HashSizeInBytes = int.Parse(metaParts[META_FORMAT_KEY_LENGTH_INDEX]),
                    Iterations = int.Parse(hashParts[HASH_FORMAT_ITERATIONS_INDEX]),
                    Salt = hashParts[HASH_FORMAT_SALT_INDEX],
                    Key = hashParts[HASH_FORMAT_KEY_INDEX],
                    NeedsUpgrade = false
                };
            }
            return null;
        }
    }
}
